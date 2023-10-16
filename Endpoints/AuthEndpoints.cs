using Models;
using Repositories;

namespace Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");
        group.MapGet("/user/{username}", GetUser);
        group.MapPost("/register", UserRegister);
        group.MapPost("/login", UserLogin);

    }

    private static async Task<IResult> UserLogin(IAuthRepository repository, User user)
    {
        try
        {
            var result = await repository.UserLoginAsync(user);
            return Results.Ok(result);
        }
        catch (Exception)
        {
            return Results.Unauthorized();
        }
    }

    public static async Task<IResult> UserRegister(IAuthRepository repository, User user){
        await repository.UserRegisterAsync(user);
        return Results.NoContent();
    }

    private static async Task<IResult> GetUser(IAuthRepository repository, string username)
    {
        var result = await repository.GetUserAsync(username);
        return Results.Ok(result);
    }

    
}