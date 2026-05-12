namespace learndotnet.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessServices(
        this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<ProductService>();
        services.AddScoped<CategoryService>();
        services.AddScoped<OrderService>();
        services.AddScoped<OrderItemService>();

        return services;
    }
}