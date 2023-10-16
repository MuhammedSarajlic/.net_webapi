using Models;
using Repositories;

namespace Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app){
        var group = app.MapGroup("api/product");
        group.MapGet("/", GetAllProducts);
        group.MapGet("/{productId}", GetProductById);
        group.MapGet("/shop/{shopId}", GetAllProductsByShopId);
        group.MapGet("/user/{userId}", GetAllProductsByUserId);
        group.MapPost("/shop/{shopId}", AddProduct);
        group.MapPut("/", EditProduct);
        group.MapDelete("/{productId}", DeleteProductById);
    }

    public static async Task<IResult> GetAllProducts(IProductRepository respository){
        var result = await respository.GetAllProductsAsync();
        return Results.Ok(result); 
    }

    private static async Task<IResult> GetProductById(IProductRepository repository, int productId)
    {
        try
        {
            var result = await repository.GetProductByIdAsync(productId);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    private static async Task<IResult> GetAllProductsByShopId(IProductRepository repository, int shopId)
    {
        try
        {
            var result = await repository.GetAllProductsByShopIdAsync(shopId);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.NotFound(e.Message);
        }
    }

    private static async Task<IResult> GetAllProductsByUserId(IProductRepository repository, int userId)
    {
        try
        {
            var result = await repository.GetAllProductsByUserIdAsync(userId);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    public static async Task<IResult> AddProduct(IProductRepository repository, Product product, int shopId){
        try
        {
            await repository.AddProductAsync(product, shopId);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            return Results.NotFound(e.Message);
        }
    }

    private static async Task<IResult> EditProduct(IProductRepository repository, Product product)
    {
        try
        {
            await repository.EditProductAsync(product);
            return Results.Ok();
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    private static async Task<IResult> DeleteProductById(IProductRepository repository, int productId)
    {
        try
        {
            await repository.DeleteProductByIdAsync(productId);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

}