using Models;
using Repositories;

namespace Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/category");
        group.MapGet("/", GetAllCategories);
        group.MapGet("/{categoryId}", GetCategoryById);
        group.MapPost("/", AddCategory);
        group.MapDelete("/{categoryId}", DeleteCategory);
    }


    public static async Task<IResult> GetAllCategories(ICategoryRepository repository){
        try
        {
            var result = await repository.GetAllCategoriesAsync();
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    private static async Task<IResult> GetCategoryById(ICategoryRepository repository, int categoryId)
    {
        try
        {
            var result = await repository.GetCategoryByIdAsync(categoryId);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    private static async Task<IResult> AddCategory(ICategoryRepository repository, Category category)
    {
        try
        {
            await repository.AddCategoryAsync(category);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    private static async Task<IResult> DeleteCategory(ICategoryRepository repository, int categoryId)
    {
        try
        {
            await repository.DeleteCategoryAsync(categoryId);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}
