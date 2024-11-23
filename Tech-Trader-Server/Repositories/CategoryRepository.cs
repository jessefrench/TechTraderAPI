using Microsoft.EntityFrameworkCore;
using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TechTraderDbContext dbContext;

        public CategoryRepository(TechTraderDbContext context)
        {
            dbContext = context;
        }

        // get all categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        // create a category
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        // update a category
        public async Task<Category> UpdateCategoryAsync(int categoryId, Category updatedCategory)
        {
            var categoryToUpdate = await dbContext.Categories.FirstOrDefaultAsync(category => category.Id == categoryId);

            if (categoryToUpdate == null)
            {
                return null;
            }

            categoryToUpdate.Name = updatedCategory.Name;

            await dbContext.SaveChangesAsync();
            return updatedCategory;
        }
    }
}