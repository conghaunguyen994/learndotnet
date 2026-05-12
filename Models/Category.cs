namespace learndotnet.Models;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    // One Category has many Products
    public List<Product> Products { get; set; } = new();
}