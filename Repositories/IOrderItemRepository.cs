using learndotnet.Models;

namespace learndotnet.Repositories;

public interface IOrderItemRepository
{
    IEnumerable<OrderItem> GetAllOrderItems();
    OrderItem? GetOrderItemById(int id);
    void AddOrderItem(OrderItem orderItem);
    void UpdateOrderItem(OrderItem orderItem);
    void DeleteOrderItem(int id);
}