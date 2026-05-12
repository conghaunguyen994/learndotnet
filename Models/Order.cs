namespace learndotnet.Models;

public class Order
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.Now;

    public decimal TotalPrice { get; set; }

    // Foreign Key
    public int UserId { get; set; }

    // Navigation Property
    public User? User { get; set; }

    // One Order has many OrderItems
    public List<OrderItem> OrderItems { get; set; } = new();
}