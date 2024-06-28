using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Project_APBD.RequestModels;
using Project_APBD.Services;

namespace Project_APBD.Endpoints;

public static class ContractEndpoints
{
    public static void RegisterContractEndpoint(this WebApplication app)
    {
        var contract = app.MapGroup("contracts");
        contract.MapPost("", CreateContract).RequireAuthorization("LoggedUser");
        contract.MapPost("/payment", ProcessPayment).RequireAuthorization("LoggedUser");
        contract.MapGet("/revenue/current", GetCurrentRevenue).RequireAuthorization("LoggedUser");
        contract.MapGet("/revenue/expected", GetExpectedRevenue).RequireAuthorization("LoggedUser");
    }
    
    
    private static async Task<IResult> CreateContract(CreateContractRequest request, IValidator<CreateContractRequest> validator, IContractService service)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }
        
        try
        {
            await service.CreateContractAsync(request);
            return Results.Created($"/contracts/{request}", request);
        }
        catch (ArgumentException e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    private static async Task<IResult> ProcessPayment(CreatePaymentRequest request, IValidator<CreatePaymentRequest> validator, IContractService service)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }
        
        try
        {
            await service.AddPaymentAsync(request);
            return Results.Ok();
        }
        catch (ArgumentException e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    private static async Task<IResult> GetCurrentRevenue(IContractService service, string currency = "PLN")
    {
        try
        {
            var revenue = await service.CalculateCurrentRevenueAsync();
            if (currency != "PLN")
            {
                revenue = await service.ConvertToCurrencyAsync(revenue, currency);
            }
            return Results.Ok(new { Revenue = revenue, Currency = currency });
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    private static async Task<IResult> GetExpectedRevenue(IContractService service, string currency = "PLN")
    {
        try
        {
            var revenue = await service.CalculateExpectedRevenueAsync();
            if (currency != "PLN")
            {
                revenue = await service.ConvertToCurrencyAsync(revenue, currency);
            }

            return Results.Ok(new { Revenue = revenue, Currency = currency });
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }

    }
}