using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Project_APBD.Contexts;
using Project_APBD.Enums;
using Project_APBD.Models;
using Project_APBD.RequestModels;

namespace Project_APBD.Services;


public interface IContractService
{
    Task CreateContractAsync(CreateContractRequest request);
    
    Task AddPaymentAsync(CreatePaymentRequest request);
    
    Task<decimal> CalculateCurrentRevenueAsync();
    
    Task<decimal> CalculateExpectedRevenueAsync();
    
    Task<decimal> ConvertToCurrencyAsync(decimal amount, string currency);

}

public class ContractService(DatabaseContext context, HttpClient client) : IContractService
{
    public async Task CreateContractAsync(CreateContractRequest request)
    {

        var software = await context.Softwares.FindAsync(request.SoftwareId);
        if (software == null)
        {
            throw new NotFoundException($"Software with ID {request.SoftwareId} not found.");
        }


        var customer = await context.Customers.FindAsync(request.CustomerId);
        if (customer == null)
        {
            throw new NotFoundException($"Customer with ID {request.CustomerId} not found.");
        }


        decimal price = request.Price + (request.SupportYears - 1) * 1000;


        var discounts = await context.Discounts
            .Where(d => d.StartDate <= DateTime.UtcNow && d.EndDate >= DateTime.UtcNow)
            .ToListAsync();

        if (discounts.Any())
        {
            var highestDiscount = discounts.Max(d => d.Value);
            price -= price * (decimal)(highestDiscount / 100);
        }


        var isReturningCustomer = await context.Contracts.AnyAsync(c => c.CustomerId == request.CustomerId);

        if (isReturningCustomer)
        {
            price -= price * 0.05m;
        }


        var contract = new Contract
        {
            SoftwareId = request.SoftwareId,
            CustomerId = request.CustomerId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Price = price,
            SupportYears = request.SupportYears,
            DiscountId = discounts.FirstOrDefault()!.Id,
            SoftwareVersion = software.CurrentVersion,
            IsSigned = request.IsSigned
        };


        context.Contracts.Add(contract);
        await context.SaveChangesAsync();
    }

    public async Task AddPaymentAsync(CreatePaymentRequest request)
    {
        var contract = await context.Contracts.Include(c => c.Payments)
            .FirstOrDefaultAsync(c => c.Id == request.ContractId);

        if (contract == null)
        {
            throw new NotFoundException($"Contract with ID {request.ContractId} not found.");
        }

        if (DateTime.UtcNow < contract.StartDate || DateTime.UtcNow > contract.EndDate)
        {
            throw new AfterTheDateException("Cannot make a payment outside of the contract's date range.");
        }

        var totalPaid = contract.Payments.Sum(p => p.Amount) + request.Amount;

        
        if (request.PaymentType == PaymentType.OneTimePayment && totalPaid != contract.Price)
        {
            throw new WrongAmountException("One-time payment must cover the entire contract price.");
        }

        if (totalPaid > contract.Price)
        {
            throw new WrongAmountException("Payment amount exceeds contract price.");
        }

        var payment = new Payment
        {
            ContractId = request.ContractId,
            Amount = request.Amount,
            Date = DateTime.UtcNow,
            PaymentType = request.PaymentType
        };

        context.Payments.Add(payment);
        await context.SaveChangesAsync();

        if (totalPaid == contract.Price)
        {
            contract.IsSigned = true;
            await context.SaveChangesAsync();
        }

    }

    public async Task<decimal> CalculateCurrentRevenueAsync()
    {
        var totalPayments = await context.Contracts
            .Where(c => c.IsSigned)
            .SumAsync(p => p.Price);

        return totalPayments;
    }

    public async Task<decimal> CalculateExpectedRevenueAsync()
    {
        var totalContractValue = await context.Contracts.SumAsync(c => c.Price);
        return totalContractValue;
    }

    public async Task<decimal> ConvertToCurrencyAsync(decimal amount, string targetCurrency)
    {
        var apiKey = "79ac4618711f47e7a75594fd";
        var baseCurrency = "PLN";
        var response = await client.GetStringAsync($"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{baseCurrency}");
        var rates = JObject.Parse(response)["conversion_rates"];
        if (rates[targetCurrency] != null)
        {
            return amount * (decimal)rates[targetCurrency];
        }
        throw new Exception("Currency conversion failed.");
    }
    
}