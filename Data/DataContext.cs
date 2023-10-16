using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;
    public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base (options)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
    }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Shop> Shops => Set<Shop>();
    public DbSet<Category> Categories => Set<Category>();

}