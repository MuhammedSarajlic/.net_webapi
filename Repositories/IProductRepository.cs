using Dtos;
using Models;

namespace Repositories;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> GetProductByIdAsync(int productId);
    Task<IEnumerable<ProductDto>> GetAllProductsByShopIdAsync(int shopId);
    Task<IEnumerable<ProductDto>> GetAllProductsByUserIdAsync(int userId);
    Task AddProductAsync(Product product, int shopId);
    Task EditProductAsync(Product product);
    Task DeleteProductByIdAsync(int productId);
}