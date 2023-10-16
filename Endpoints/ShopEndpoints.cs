using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

namespace Endpoints;

public static class ShopEndpoints
{
    public static void MapShopEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/shop");
        group.MapPost("/{id}", AddShop);
        group.MapGet("/", GetAllShops);
        group.MapGet("/user/{userId}", GetShopsByUserId);
        group.MapGet("/{shopId}", GetShopById);
        group.MapGet("/category", GetShopsByCategory);
        group.MapPut("/", EditShop);
        group.MapDelete("/{shopId}", DeleteShop);
    }

    private static async Task<IResult> GetAllShops(IShopRepository repository)
    {
        try
        {
            var result = await repository.GetAllShopsAsync();
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    public static async Task<IResult> AddShop(IShopRepository repository, Shop shop, int id){
        try{
            await repository.AddShopAsync(shop, id);
            return Results.Ok();

        }catch(Exception e){
            return Results.BadRequest(e.Message);
        }
    }

    public static async Task<IResult> GetShopsByUserId(IShopRepository repository, int userId){
        try
        {
            var result = await repository.GetShopsByUserIdAsync(userId);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.NotFound(e.Message);
        }
    }

    public static async Task<IResult> GetShopById(IShopRepository repository, int shopId)
    {
        try
        {
            var result = await repository.GetShopByIdAsync(shopId);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.NotFound(e.Message);
        }
    }

    public static async Task<IResult> GetShopsByCategory(IShopRepository repository, [FromQuery]string category)
    {
        try
        {
            var result = await repository.GetShopByCategoryAsync(category);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.NotFound(e.Message);
        }
    }

    public static async Task<IResult> EditShop(IShopRepository repository, Shop shop)
    {
        try
        {
            await repository.EditShopAsync(shop);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            return Results.NotFound(e.Message);
        }
    }

    public static async Task<IResult> DeleteShop(IShopRepository repository, int shopId)
    {
        try
        {
            await repository.DeleteShopAsync(shopId);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            return Results.NotFound(e.Message);
        }
    }
}