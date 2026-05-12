using learndotnet.Models;
using learndotnet.Repositories;

namespace learndotnet.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public IEnumerable<Order> GetOrders()
    {
        return _orderRepository.GetAllOrders();
    }

    public Order? GetOrderById(int id)
    {
        return _orderRepository.GetOrderById(id);
    }

    public void AddOrder(Order order)
    {
        _orderRepository.AddOrder(order);
    }

    public void UpdateOrder(Order order)
    {
        _orderRepository.UpdateOrder(order);
    }

    public void DeleteOrder(int id)
    {
        _orderRepository.DeleteOrder(id);
    }
    public int GetOrderCountForToday()
    {
        var today = DateTime.Today;
        return _orderRepository.GetAllOrders().Count(o => o.OrderDate.Date == today);
    }
}