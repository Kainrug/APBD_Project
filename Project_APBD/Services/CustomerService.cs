using Microsoft.EntityFrameworkCore;
using Project_APBD.Contexts;
using Project_APBD.Models;
using Project_APBD.RequestModels;
using Project_APBD.ResponseModels;

namespace Project_APBD.Services;


public interface ICustomerService
{
    Task<GetCustomerResponse> GetCustomerByIdAsync(int id);
    
    Task CreateIndividualCustomerAsync(CreateIndividualCustomerRequest request);
    
    Task CreateCompanyCustomerAsync(CreateCompanyCustomerRequest request);
    
    Task UpdateIndividualCustomerAsync(int id, UpdateIndividualCustomerRequest request);
    
    
    Task UpdateCompanyCustomerAsync(int id, UpdateCompanyCustomerRequest request);
    Task DeleteCustomerAsync(int id);
}

public class CustomerService(DatabaseContext context) : ICustomerService
{
    public async Task<GetCustomerResponse> GetCustomerByIdAsync(int id)
    {
        var customer = await context.Customers.FindAsync(id);
        if (customer == null)
        {
            throw new NotFoundException($"Customer with ID {id} not found.");
        }

        if (customer.IsDeleted)
        {
            throw new NotFoundException("This client is deleted, you can't view him");
        }

        if (customer is IndividualCustomer individualCustomer)
        {
            return new GetCustomerResponse
            {
                Id = customer.Id,
                Address = customer.Address,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                FirstName = individualCustomer.FirstName,
                LastName = individualCustomer.LastName,
                Pesel = individualCustomer.Pesel
            };
        }

        if (customer is CompanyCustomer companyCustomer)
        {
            return new GetCustomerResponse
            {
                Id = customer.Id,
                Address = customer.Address,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                CompanyName = companyCustomer.CompanyName,
                KrsNumber = companyCustomer.KrsNumber
            };
        }
        return null;
    }

    public async Task CreateIndividualCustomerAsync(CreateIndividualCustomerRequest request)
    {
        var customer = new IndividualCustomer
        {
            Address = request.Address,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Pesel = request.Pesel
        };

        await context.IndividualCustomers.AddAsync(customer);
        await context.SaveChangesAsync();
    }

    public async Task CreateCompanyCustomerAsync(CreateCompanyCustomerRequest request)
    {
        var customer = new CompanyCustomer
        {
            Address = request.Address,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            CompanyName = request.CompanyName,
            KrsNumber = request.KrsNumber
        };

        await context.CompanyCustomers.AddAsync(customer);
        await context.SaveChangesAsync();
    }

    public async Task UpdateIndividualCustomerAsync(int id, UpdateIndividualCustomerRequest request)
    {
        var customer = await context.IndividualCustomers
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

        if (customer == null)
        {
            throw new NotFoundException($"Individual Customer with ID {id} not found.");
        }

        customer.Address = request.Address;
        customer.Email = request.Email;
        customer.PhoneNumber = request.PhoneNumber;
        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;

        context.IndividualCustomers.Update(customer);
        await context.SaveChangesAsync();
    }

    public async Task UpdateCompanyCustomerAsync(int id, UpdateCompanyCustomerRequest request)
    {
        var customer = await context.CompanyCustomers
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

        if (customer == null)
        {
            throw new NotFoundException($"Company Customer with ID {id} not found.");
        }

        customer.Address = request.Address;
        customer.Email = request.Email;
        customer.PhoneNumber = request.PhoneNumber;
        customer.CompanyName = request.CompanyName;

        context.CompanyCustomers.Update(customer);
        await context.SaveChangesAsync();
    }

    

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await context.Customers
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null)
        {
            throw new NotFoundException($"Customer with id: {id} not found");
        }

        if (customer is CompanyCustomer)
        {
            throw new BadRequestException("Company customers cannot be deleted");
        }

        customer.IsDeleted = true;

        context.Customers.Update(customer);
        await context.SaveChangesAsync();
    }
}