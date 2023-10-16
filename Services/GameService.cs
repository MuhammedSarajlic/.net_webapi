using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;

namespace Services;

public class GameService : IGameRespository
{
    private readonly DataContext context;
    public GameService(DataContext context)
    {
        this.context = context;
    }
    public async Task<IEnumerable<Game>> GetAllGamesAsync()
    {
        return await context.Games.AsNoTracking().ToListAsync();
    }

    public async Task<Game?> GetGameByIdAsync(int id)
    {
        return await context.Games.FindAsync(id);
    }
    public async Task AddGameAsync(Game game)
    {
        await context.Games.AddAsync(game);
        await context.SaveChangesAsync();
    }
    public async Task UpdateGameAsync(Game game)
    {
        context.Update(game);
        await context.SaveChangesAsync();
    }
    public async Task DeleteGameAsync(int id)
    {
        await context.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
    }
}