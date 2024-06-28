using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Project_APBD.RequestModels;
using Project_APBD.Services;

namespace Project_APBD.Endpoints;

public static class UserEndpoints
{
    public static void RegisterUserEndpoint(this WebApplication app)
    {
        var user = app.MapGroup("users");
        user.MapPost("register", RegisterUser).AllowAnonymous();
        user.MapPost("login", LoginUser).AllowAnonymous();
        user.MapPost("refreshToken", RefreshToken).AllowAnonymous();
    }
    
    private static async Task<IResult> RegisterUser(RegisterRequest request, IValidator<RegisterRequest> validator, IUserService service)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }
        
        try
        {
            await service.CreateUserAsync(request);
            return Results.Created();
        }
        catch (ArgumentException e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    public static async Task<IResult> LoginUser(LoginRequest loginRequestModel, IUserService service)
    { 
        var response = await service.LoginAsync(loginRequestModel);
        if (response != null)
        {
            return Results.Ok(response);
        }
        return Results.Unauthorized();
    }
    
    public static async Task<IResult> RefreshToken(string refreshToken, IUserService service)
    { 
        var result = await service.RefreshTokenAsync(refreshToken);
        if (result == null)
        {
            return Results.Unauthorized();
        }
        return Results.Ok(result);
    }
}