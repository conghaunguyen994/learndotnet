using learndotnet.Models;
using learndotnet.Services;

namespace learndotnet.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        // GET /categories
        app.MapGet("/categories", (CategoryService categoryService) =>
        {
            return categoryService.GetCategories();
        }).WithName("GetAllCategories").RequireAuthorization();

        // GET /categories/{id}
        app.MapGet("/categories/{id}", (int id, CategoryService categoryService) =>
        {
            var category = categoryService.GetCategoryById(id);
            return category is not null ? Results.Ok(category) : Results.NotFound();
        }).WithName("GetCategoryById").RequireAuthorization();

        // POST /categories
        app.MapPost("/categories", (Category category, CategoryService categoryService) =>
        {
            categoryService.AddCategory(category);
            return Results.Created($"/categories/{category.Id}", category);
        }).WithName("CreateCategory").RequireAuthorization();

        // PUT /categories/{id}
        app.MapPut("/categories/{id}", (int id, Category updatedCategory, CategoryService categoryService) =>
        {
            var existingCategory = categoryService.GetCategoryById(id);
            if (existingCategory is null)
            {
                return Results.NotFound();
            }

            updatedCategory.Id = id;
            categoryService.UpdateCategory(updatedCategory);
            return Results.NoContent();
        }).WithName("UpdateCategory").RequireAuthorization();

        // DELETE /categories/{id}
        app.MapDelete("/categories/{id}", (int id, CategoryService categoryService) =>
        {
            var category = categoryService.GetCategoryById(id);
            if (category is null)
            {
                return Results.NotFound();
            }

            categoryService.DeleteCategory(id);
            return Results.NoContent();
        }).WithName("DeleteCategory").RequireAuthorization();
    }
}