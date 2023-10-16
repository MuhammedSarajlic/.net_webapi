using Data;
using Dtos;
using Exceptions;
using Extensions;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;

namespace Services;

public class ProductService : IProductRepository
{
    private readonly DataContext context;
    private readonly IShopRepository shopRepository;
    public ProductService(DataContext context, IShopRepository shopRepository)
    {
        this.context = context;
        this.shopRepository = shopRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(){
        return await context.Products.AsNoTracking().Include(p => p.Shop).Select(p => p.AsDto()).ToListAsync();
    }

    public async Task<ProductDto> GetProductByIdAsync(int productId){
        return await context.Products.Where(p => p.Id == productId).Select(p => p.AsDto()).FirstOrDefaultAsync() ?? throw new ProductNotFoundException();
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsByShopIdAsync(int shopId)
    {
        var shop = await context.Shops.FindAsync(shopId) ?? throw new ShopNotFoundException();
        var shopProductsCollection = await context.Products.Where(p => p.ShopId == shopId).Select(p => p.AsDto()).ToListAsync();
        return shopProductsCollection;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsByUserIdAsync(int userId)
    {
        var user = await context.Users.FindAsync(userId) ?? throw new UserNotFoundException();

        List<ProductDto> userProductCollection = new();

        var userShopsCollection = await shopRepository.GetShopsByUserIdAsync(userId);
        foreach(var shop in userShopsCollection){
            var products = await context.Products.Where(p => p.ShopId == shop.Id).Select(p => p.AsDto()).ToListAsync() ?? throw new ProductNotFoundException();
            userProductCollection.AddRange(products); 
        }

        return userProductCollection;
    }

    public async Task AddProductAsync(Product product, int shopId)
    {
        var shop = await shopRepository.GetShopByIdAsync(shopId);

        product.ShopId = shopId;
        await context.Products.AddAsync(product);
        shop.Products.Add(product.AsDto());

        await context.SaveChangesAsync();
    }

    public async Task EditProductAsync(Product product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();
    }

    public async Task DeleteProductByIdAsync(int productId)
    {
        var product = await context.Products.FindAsync(productId) ?? throw new ProductNotFoundException();
        await context.Products.Where(p => p.Id == productId).ExecuteDeleteAsync();
    }
}