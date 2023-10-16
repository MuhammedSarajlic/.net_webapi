using Data;
using Dtos;
using Exceptions;
using Extensions;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;

namespace Services;

public class ShopService : IShopRepository
{
    private readonly DataContext context;
    public ShopService(DataContext context)
    {
        this.context = context;
    }

    public async Task AddShopAsync(Shop shop, int ownerId)
    {
        var user = await context.Users.Where(u => u.Id == ownerId).Include(u => u.OwnedShops).FirstOrDefaultAsync() ?? throw new UserNotFoundException();
        shop.ShopOwnerId = ownerId;
        await context.Shops.AddAsync(shop);

        user.OwnedShops?.Add(shop);

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ShopDto>> GetAllShopsAsync()
    {
        var shops = await context.Shops.AsNoTracking().Include(s => s.Products).Select(s => s.AsDto()).ToListAsync();
        return shops;
    }

    public async Task<IEnumerable<ShopDto>> GetShopsByUserIdAsync(int userId)
    {
        var user = await context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync() ?? throw new UserNotFoundException(); 
        var userShops = await context.Shops.Where(s => s.ShopOwnerId == userId).Select(shop => shop.AsDto()).ToListAsync();
        return userShops;
    }

    public async Task<ShopDto> GetShopByIdAsync(int shopId){
        var shop = await context.Shops.Include(s => s.Products).Where(s => s.Id == shopId).Select(s => s.AsDto()).FirstOrDefaultAsync() ?? throw new ShopNotFoundException();
        return shop;
    }

    public async Task<IEnumerable<ShopDto>> GetShopByCategoryAsync(string category)
    {
        var shopByCategory = await context.Shops
            .Where(s => s.Category != null && s.Category.Name.ToLower() == category)
            .Select(s => s.AsDto())
            .ToListAsync() ?? throw new CategoryNotFoundException();
        return shopByCategory;
    }


    public async Task EditShopAsync(Shop shop)
    {
        context.Update(shop);
        await context.SaveChangesAsync();
    }

    public async Task DeleteShopAsync(int shopId)
    {
        var shop = await context.Shops.FindAsync(shopId) ?? throw new ShopNotFoundException();
        context.Shops.Remove(shop);
        await context.SaveChangesAsync();
    }

}