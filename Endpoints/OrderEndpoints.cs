using learndotnet.Models;
using learndotnet.Services;

namespace learndotnet.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        // GET /orders
        app.MapGet("/orders", (OrderService orderService) =>
        {
            return orderService.GetOrders();
        }).WithName("GetAllOrders");

        // GET /orders/{id}
        app.MapGet("/orders/{id}", (int id, OrderService orderService) =>
        {
            var order = orderService.GetOrderById(id);
            return order is not null ? Results.Ok(order) : Results.NotFound();
        }).WithName("GetOrderById");

        // POST /orders
        app.MapPost("/orders", (Order order, OrderService orderService) =>
        {
            orderService.AddOrder(order);
            return Results.Created($"/orders/{order.Id}", order);
        }).WithName("CreateOrder");

         app.MapGet("/orders/count/today", (OrderService orderService) =>
        {
            var count = orderService.GetOrderCountForToday();
            return Results.Ok(new { Date = DateTime.Today, OrderCount = count });
        }).WithName("GetOrderCountForToday");

        // PUT /orders/{id}
        app.MapPut("/orders/{id}", (int id, Order updatedOrder, OrderService orderService) =>
        {
            var existingOrder = orderService.GetOrderById(id);
            if (existingOrder is null)
            {
                return Results.NotFound();
            }

            updatedOrder.Id = id;
            orderService.UpdateOrder(updatedOrder);
            return Results.NoContent();
        }).WithName("UpdateOrder");

        // DELETE /orders/{id}
        app.MapDelete("/orders/{id}", (int id, OrderService orderService) =>
        {
            var order = orderService.GetOrderById(id);
            if (order is null)
            {
                return Results.NotFound();
            }

            orderService.DeleteOrder(id);
            return Results.NoContent();
        }).WithName("DeleteOrder");
    }
    
}