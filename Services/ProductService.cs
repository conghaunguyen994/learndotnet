using learndotnet.Models;
using learndotnet.Repositories;

namespace learndotnet.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IEnumerable<Product> GetProducts()
    {
        return _productRepository.GetAllProducts();
    }

    public Product? GetProductById(int id)
    {
        return _productRepository.GetProductById(id);
    }
    public Product CreateProduct(Product product)
    {
        return _productRepository.CreateProduct(product);
    }
    public Product? UpdateProduct(int id, Product product)
    {
        return _productRepository.UpdateProduct(id, product);
    }
    public Product? DeleteProduct(int id)
    {
        return _productRepository.DeleteProduct(id);
    }
    
}