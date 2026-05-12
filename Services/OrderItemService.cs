using learndotnet.Models;
using learndotnet.Repositories;

namespace learndotnet.Services;

public class OrderItemService
{
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderItemService(IOrderItemRepository orderItemRepository)
    {
        _orderItemRepository = orderItemRepository;
    }

    public IEnumerable<OrderItem> GetOrderItems()
    {
        return _orderItemRepository.GetAllOrderItems();
    }

    public OrderItem? GetOrderItemById(int id)
    {
        return _orderItemRepository.GetOrderItemById(id);
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        _orderItemRepository.AddOrderItem(orderItem);
    }

    public void UpdateOrderItem(OrderItem orderItem)
    {
        _orderItemRepository.UpdateOrderItem(orderItem);
    }

    public void DeleteOrderItem(int id)
    {
        _orderItemRepository.DeleteOrderItem(id);
    }
}