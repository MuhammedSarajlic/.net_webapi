using Data;
using Repositories;
using Services;

namespace Extensions;

public static class ServiceExtension
{
    public static void AddCustomServices(this IServiceCollection services){
        services.AddDbContext<DataContext>();
        services.AddScoped<IGameRespository, GameService>();
        services.AddScoped<IProductRepository, ProductService>();
        services.AddScoped<IAuthRepository, AuthService>();
        services.AddScoped<IShopRepository, ShopService>();
        services.AddScoped<ICategoryRepository, CategoryService>();
    }
}