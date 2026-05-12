using learndotnet.Data;
using learndotnet.Models;

namespace learndotnet.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly AppDbContext _context;

    public OrderItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<OrderItem> GetAllOrderItems()
    {
        return _context.OrderItems.ToList();
    }

    public OrderItem? GetOrderItemById(int id)
    {
        return _context.OrderItems.FirstOrDefault(oi => oi.Id == id);
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        _context.SaveChanges();
    }

    public void UpdateOrderItem(OrderItem orderItem)
    {
        _context.OrderItems.Update(orderItem);
        _context.SaveChanges();
    }

    public void DeleteOrderItem(int id)
    {
        var orderItem = _context.OrderItems.Find(id);
        if (orderItem != null)
        {
            _context.OrderItems.Remove(orderItem);
            _context.SaveChanges();
        }
    }
}