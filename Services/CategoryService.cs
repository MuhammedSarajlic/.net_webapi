using Data;
using Exceptions;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;

namespace Services;

public class CategoryService : ICategoryRepository
{
    private readonly DataContext context;
    public CategoryService(DataContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await context.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int categoryId)
    {
        return await context.Categories.AsNoTracking().Where(c => c.Id == categoryId).FirstAsync() ?? throw new CategoryNotFoundException();
    }

    public async Task AddCategoryAsync(Category category)
    {
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        await context.Categories.Where(c => c.Id == categoryId).ExecuteDeleteAsync();
    }

    
}