using Models;
using Repositories;

namespace Endpoints;

public static class GameEndpoints
{
    const string GetGameEndpointName = "GetGame";
    public static void MapGameEndpoints(this IEndpointRouteBuilder app){
        var group = app.MapGroup("api/games");
        group.MapGet("/", GetAllGames);
        group.MapGet("/{id}", GetGameById).WithName(GetGameEndpointName);
        group.MapPost("/", AddGame);
        group.MapPut("/{id}", UpdateGameAsync);
        group.MapDelete("/{id}", DeleteGame);
    }

    public static async Task<IResult> GetAllGames(IGameRespository respository){
        var result = await respository.GetAllGamesAsync();
        return Results.Ok(result);
    }

    public static async Task<IResult> GetGameById(IGameRespository respository, int id)
    {
        var result = await respository.GetGameByIdAsync(id);
        return Results.Ok(result);
    }

    public static async Task<IResult> AddGame(IGameRespository respository, Game game){
        await respository.AddGameAsync(game);
        return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
    }

    public static async Task<IResult> UpdateGameAsync(IGameRespository respository, int id, Game game){
        Game? existingGame = await respository.GetGameByIdAsync(id);
        if(existingGame is null){
            return Results.NotFound();
        }
        existingGame.Name = game.Name;
        existingGame.Genre = game.Genre;
        existingGame.Price = game.Price;
        existingGame.ReleaseDate = game.ReleaseDate;

        await respository.UpdateGameAsync(existingGame);
        return Results.NoContent();
    }

    public static async Task<IResult> DeleteGame(IGameRespository respository, int id){
        await respository.DeleteGameAsync(id);
        return Results.NoContent();
    }
}