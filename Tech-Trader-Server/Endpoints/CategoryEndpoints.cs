using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public class CategoryEndpoints
    {
        public static void Map(WebApplication app)
        {
            // get all categories
            app.MapGet("/categories", async (ICategoryService categoryService) =>
            {
                return await categoryService.GetCategoriesAsync();
            })
            .Produces<List<Category>>(StatusCodes.Status200OK);

            // get a single category by id
            app.MapGet("/categories/{categoryId}", async (ICategoryService categoryService, int categoryId) =>
            {
                Category selectedCategory = await categoryService.GetCategoryByIdAsync(categoryId);
                return Results.Ok(selectedCategory);
            })
            .Produces<Category>(StatusCodes.Status200OK);

            // create a new category
            app.MapPost("/categories", async (ICategoryService categoryService, Category category) =>
            {
                var newCategory = await categoryService.CreateCategoryAsync(category);
                return Results.Created($"/categories/{category.Id}", category);
            })
            .Produces<Category>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}