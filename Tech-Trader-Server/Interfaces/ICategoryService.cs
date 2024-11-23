using TechTrader.Models;

namespace TechTrader.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> CreateCategoryAsync(Category Category);
        Task<Category> UpdateCategoryAsync(int categoryId, Category updatedCategory);
    }
}