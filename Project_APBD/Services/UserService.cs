using Microsoft.EntityFrameworkCore;
using Project_APBD.Contexts;
using Project_APBD.Enums;
using Project_APBD.Helpers;
using Project_APBD.Models;
using Project_APBD.RequestModels;
using Project_APBD.ResponseModels;

namespace Project_APBD.Services;


public interface IUserService
{
    Task CreateUserAsync(RegisterRequest request);
    
    Task<LoginResponse> LoginAsync(LoginRequest model);
    
    Task<LoginResponse> RefreshTokenAsync(string refreshToken);
    
}

public class UserService(DatabaseContext context, IConfiguration config) : IUserService
{
    public async Task CreateUserAsync(RegisterRequest request)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(request.Password);
        

        var user = new User()
        {
            Login = request.Login,
            Email = request.Email,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1),
            UserRoles = UserRoles.LoggedUser
        };
        context.Users.Add(user); 
        context.SaveChanges();
        
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest model)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
        if (user is null || !SecurityHelpers.CheckIfPasswordIsCorrect(user.Password, model.Password, user.Salt))
        {
            return null;
        }
        
        var token = SecurityHelpers.GenerateJwtToken(user, config);
        var refreshToken = SecurityHelpers.GenerateRefreshToken();
            
        context.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiryDate = DateTime.Now.AddDays(3),
            UserId = user.Id
        });
        await context.SaveChangesAsync();

        return new LoginResponse
        {
            Token = token,
            RefreshToken = refreshToken
        };
    }

    public async Task<LoginResponse> RefreshTokenAsync(string refreshToken)
    {
        var storedToken = await context.RefreshTokens.Include(rt => rt.User)
            .SingleOrDefaultAsync(rt => rt.Token == refreshToken);
        
        if (storedToken == null || storedToken.ExpiryDate <= DateTime.Now) return null;
        
        var user = storedToken.User;
        var newJwtToken = SecurityHelpers.GenerateJwtToken(user, config);
        var newRefreshToken = SecurityHelpers.GenerateRefreshToken();

        storedToken.Token = newRefreshToken;
        storedToken.ExpiryDate = DateTime.Now.AddDays(3);
        context.RefreshTokens.Update(storedToken);
        await context.SaveChangesAsync();

        return new LoginResponse()
        {
            Token = newJwtToken,
            RefreshToken = newRefreshToken
        };
    }
}