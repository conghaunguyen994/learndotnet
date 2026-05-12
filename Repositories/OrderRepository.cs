using learndotnet.Data;
using learndotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace learndotnet.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Order> GetAllOrders()
    {
        return _context.Orders.Include(o => o.OrderItems).ToList();
    }

    public Order? GetOrderById(int id)
    {
        return _context.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);
    }

    public void AddOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public void UpdateOrder(Order order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();
    }

    public void DeleteOrder(int id)
    {
        var order = _context.Orders.Find(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}