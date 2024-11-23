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

            // create a new category
            app.MapPost("/categories", async (ICategoryService categoryService, Category category) =>
            {
                var newCategory = await categoryService.CreateCategoryAsync(category);
                return Results.Created($"/categories/{category.Id}", category);
            })
            .Produces<Category>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // update a category
            app.MapPut("/categories/{categoryId}", async (ICategoryService categoryService, int categoryId, Category updatedCategory) =>
            {
                var categoryToUpdate = await categoryService.UpdateCategoryAsync(categoryId, updatedCategory);
                return Results.Ok(categoryToUpdate);
            })
            .Produces<Category>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);
        }
    }
}