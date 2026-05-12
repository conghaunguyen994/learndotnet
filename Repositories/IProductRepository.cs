using learndotnet.Models;

namespace learndotnet.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();

    Product? GetProductById(int id);
    Product CreateProduct(Product product);

    Product? UpdateProduct(int id, Product product);

    Product? DeleteProduct(int id);
}