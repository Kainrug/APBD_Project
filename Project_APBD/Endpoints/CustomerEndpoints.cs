using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Project_APBD.RequestModels;
using Project_APBD.Services;

namespace Project_APBD.Endpoints;

public static class CustomerEndpoints
{
    public static void RegisterCustomerEndpoint(this WebApplication app)
    {
        var customer = app.MapGroup("customers");
        customer.MapGet("{id:int}", GetCustomerById).RequireAuthorization("LoggedUser");
        customer.MapPost("individual", CreateIndividualCustomer).RequireAuthorization("LoggedUser");
        customer.MapPost("company", CreateCompanyCustomer).RequireAuthorization("LoggedUser");
        customer.MapPut("individual/{id:int}", UpdateIndividualCustomer).RequireAuthorization("LoggedUser");
        customer.MapPut("company/{id:int}", UpdateCompanyCustomer).RequireAuthorization("LoggedUser");
        customer.MapDelete("{id:int}", DeleteCustomer).RequireAuthorization("LoggedUser");
    }

    
    private static async Task<IResult> GetCustomerById(int id, ICustomerService db)
    {
        try
        {
            return Results.Ok(await db.GetCustomerByIdAsync(id));
        }
        catch (NotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
    }

    private static async Task<IResult> CreateIndividualCustomer(CreateIndividualCustomerRequest request, IValidator<CreateIndividualCustomerRequest> validator, ICustomerService service)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }
        
        try
        {
            await service.CreateIndividualCustomerAsync(request);
            return Results.Created();
        }
        catch (ArgumentException e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    private static async Task<IResult> CreateCompanyCustomer(CreateCompanyCustomerRequest request, IValidator<CreateCompanyCustomerRequest> validator, ICustomerService service)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }
        
        try
        {
            await service.CreateCompanyCustomerAsync(request);
            return Results.Created();
        }
        catch (ArgumentException e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    private static async Task<IResult> UpdateIndividualCustomer(int id, UpdateIndividualCustomerRequest request, IValidator<UpdateIndividualCustomerRequest> validator, ICustomerService service)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        try
        {
            await service.UpdateIndividualCustomerAsync(id, request);
            return Results.Ok("Individual customer updated successfully.");
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<IResult> UpdateCompanyCustomer(int id, UpdateCompanyCustomerRequest request, IValidator<UpdateCompanyCustomerRequest> validator, ICustomerService service)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        try
        {
            await service.UpdateCompanyCustomerAsync(id, request);
            return Results.Ok("Company customer updated successfully.");
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<IResult> DeleteCustomer(int id, ICustomerService service)
    {
        try
        {
            await service.DeleteCustomerAsync(id);
            return Results.NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}