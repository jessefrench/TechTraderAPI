using TechTrader.Models;

namespace TechTrader.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<Category> CreateCategoryAsync(Category Category);
    }
}