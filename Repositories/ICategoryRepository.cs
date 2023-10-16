using Models;

namespace Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int categoryId);
    Task AddCategoryAsync(Category category);
    Task DeleteCategoryAsync(int categoryId);
}