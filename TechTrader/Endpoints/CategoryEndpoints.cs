using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/categories").WithTags(nameof(Category));

            // get all categories
            group.MapGet("/", async (ICategoryService categoryService) =>
            {
                return await categoryService.GetCategoriesAsync();
            })
            .WithName("GetCategories")
            .WithOpenApi()
            .Produces<List<Category>>(StatusCodes.Status200OK);

            // create a new category
            group.MapPost("/", async (ICategoryService categoryService, Category category) =>
            {
                var newCategory = await categoryService.CreateCategoryAsync(category);
                return Results.Created($"/categories/{category.Id}", category);
            })
            .WithName("CreateCategory")
            .WithOpenApi()
            .Produces<Category>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // update a category
            group.MapPut("/{categoryId}", async (ICategoryService categoryService, int categoryId, Category updatedCategory) =>
            {
                var categoryToUpdate = await categoryService.UpdateCategoryAsync(categoryId, updatedCategory);
                return Results.Ok(categoryToUpdate);
            })
            .WithName("UpdateCategory")
            .WithOpenApi()
            .Produces<Category>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);
        }
    }
}