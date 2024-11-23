using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        { 
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetCategoriesAsync();
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            return await _categoryRepository.CreateCategoryAsync(category);
        }

        public async Task<Category> UpdateCategoryAsync(int categoryId, Category updatedCategory)
        {
            return await _categoryRepository.UpdateCategoryAsync(categoryId, updatedCategory);
        }
    }
}