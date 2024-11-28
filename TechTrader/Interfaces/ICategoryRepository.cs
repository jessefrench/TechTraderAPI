using TechTrader.Models;

namespace TechTrader.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> CreateCategoryAsync(Category Category);
        Task<Category> UpdateCategoryAsync(int categoryId, Category updatedCategory);
    }
}