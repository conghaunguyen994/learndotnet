using learndotnet.Models;
using learndotnet.Services;

namespace learndotnet.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var productGroup = app.MapGroup("/products").WithTags("Products");
        productGroup.MapGet("/", (ProductService productService) =>
        {
            return productService.GetProducts();
        }).RequireAuthorization();

        productGroup.MapGet("/{id}", (int id, ProductService productService) =>
        {
            var product = productService.GetProductById(id);

            return product is not null
                ? Results.Ok(product)
                : Results.NotFound();
        }).RequireAuthorization();
        productGroup.MapPost("/", (Product product, ProductService productService) =>
        {
            var createdProduct = productService.CreateProduct(product);

            return Results.Created(
                $"/products/{createdProduct.Id}",
                createdProduct
            );
        }).RequireAuthorization();
        productGroup.MapPut("/{id}", (int id, Product product, ProductService productService) =>
        {
            var updatedProduct = productService.UpdateProduct(id, product);

            return updatedProduct is not null
                ? Results.Ok(updatedProduct)
                : Results.NotFound();
        }).RequireAuthorization();
        productGroup.MapDelete("/{id}", (int id, ProductService productService) =>
        {
            var deletedProduct = productService.DeleteProduct(id);

            return deletedProduct is not null
                ? Results.Ok(deletedProduct)
                : Results.NotFound();
        }).RequireAuthorization();
    }

}