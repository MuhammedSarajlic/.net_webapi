using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data;
using Dtos;
using Exceptions;
using Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories;

namespace Services;

public class AuthService : IAuthRepository
{
    private readonly DataContext context;
    public AuthService(DataContext context)
    {
        this.context = context;
    }

    public async Task<UserDto> GetUserAsync(string username)
    {
        User user = await context.Users.Where(u => u.Username == username).FirstAsync() ?? throw new UserNotFoundException();
        string token = GenerateToken(user);
        UserDto userDto = await context.Users.Where(u => u.Username == username).Select(u => u.AsDto(token)).FirstAsync() ?? throw new UserNotFoundException();
        return userDto;
    }

    public async Task<UserDto> UserLoginAsync(User user)
    {
        var userDb = await context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
        if(userDb == null || !VerifyPassword(user.PasswordHash, userDb.PasswordHash)){
            throw new UserNotFoundException();
        }
        string token = GenerateToken(user);
        UserDto userDto = userDb.AsDto(token);
        return userDto;
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("uMn/fmJXFB/KHzoGf3Z54YgyTeLTgcirda2+zC6svSo=");
        var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(new[]{
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private bool VerifyPassword(string enteredPassword, string userPassword)
    {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, userPassword);
    }

    public async Task UserRegisterAsync(User user)
    {
        user.PasswordHash = HashPassword(user.PasswordHash);
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    private string HashPassword(string password)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt(6);
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
        return hashedPassword;
    }
}