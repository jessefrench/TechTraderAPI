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

        // get a single category by id
        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            Category selectedCategory = await dbContext.Categories.FirstOrDefaultAsync(category => category.Id == categoryId);
            return selectedCategory;
        }

        // create a category
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }
    }
}