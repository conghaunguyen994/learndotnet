using learndotnet.Models;
using learndotnet.Services;

namespace learndotnet.Endpoints;

public static class OrderItemEndpoints
{
    public static void MapOrderItemEndpoints(this WebApplication app)
    {
        // GET /orderitems
        app.MapGet("/orderitems", (OrderItemService orderItemService) =>
        {
            return orderItemService.GetOrderItems();
        }).WithName("GetAllOrderItems");

        // GET /orderitems/{id}
        app.MapGet("/orderitems/{id}", (int id, OrderItemService orderItemService) =>
        {
            var orderItem = orderItemService.GetOrderItemById(id);
            return orderItem is not null ? Results.Ok(orderItem) : Results.NotFound();
        }).WithName("GetOrderItemById");

        // POST /orderitems
        app.MapPost("/orderitems", (OrderItem orderItem, OrderItemService orderItemService) =>
        {
            orderItemService.AddOrderItem(orderItem);
            return Results.Created($"/orderitems/{orderItem.Id}", orderItem);
        }).WithName("CreateOrderItem");

        // PUT /orderitems/{id}
        app.MapPut("/orderitems/{id}", (int id, OrderItem updatedOrderItem, OrderItemService orderItemService) =>
        {
            var existingOrderItem = orderItemService.GetOrderItemById(id);
            if (existingOrderItem is null)
            {
                return Results.NotFound();
            }

            updatedOrderItem.Id = id;
            orderItemService.UpdateOrderItem(updatedOrderItem);
            return Results.NoContent();
        }).WithName("UpdateOrderItem");

        // DELETE /orderitems/{id}
        app.MapDelete("/orderitems/{id}", (int id, OrderItemService orderItemService) =>
        {
            var orderItem = orderItemService.GetOrderItemById(id);
            if (orderItem is null)
            {
                return Results.NotFound();
            }

            orderItemService.DeleteOrderItem(id);
            return Results.NoContent();
        }).WithName("DeleteOrderItem");
    }
}