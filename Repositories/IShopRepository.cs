using Dtos;
using Models;

namespace Repositories;

public interface IShopRepository
{
    Task AddShopAsync(Shop shop, int ownerId);
    Task<IEnumerable<ShopDto>> GetAllShopsAsync();
    Task<IEnumerable<ShopDto>> GetShopsByUserIdAsync(int userId);
    Task<ShopDto> GetShopByIdAsync(int shopId);
    Task<IEnumerable<ShopDto>> GetShopByCategoryAsync(string category);
    Task EditShopAsync(Shop shop);
    Task DeleteShopAsync(int shopId);
}