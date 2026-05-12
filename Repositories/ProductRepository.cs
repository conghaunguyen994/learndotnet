using learndotnet.Data;
using learndotnet.Models;

namespace learndotnet.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }

    public Product? GetProductById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }
    public Product CreateProduct(Product product)
    {
        _context.Products.Add(product);

        _context.SaveChanges();

        return product;
    }
    public Product? UpdateProduct(int id, Product product)
    {
        var existingProduct = _context.Products.FirstOrDefault(p => p.Id == id);

        if (existingProduct is null)
        {
            return null;
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;
        existingProduct.CategoryId = product.CategoryId;

        _context.SaveChanges();

        return existingProduct;
    }
    public Product? DeleteProduct(int id)
    {
        var existingProduct = _context.Products.FirstOrDefault(p => p.Id == id);

        if (existingProduct is null)
        {
            return null;
        }

        _context.Products.Remove(existingProduct);
        _context.SaveChanges();

        return existingProduct;
    }
}