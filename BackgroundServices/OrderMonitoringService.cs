using learndotnet.Services;

namespace learndotnet.BackgroundServices;

public class OrderMonitoringService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OrderMonitoringService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromHours(1); // Check every hour

    public OrderMonitoringService(IServiceProvider serviceProvider, ILogger<OrderMonitoringService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Order Monitoring Service is starting.");

        using var timer = new PeriodicTimer(_interval);

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var orderService = scope.ServiceProvider.GetRequiredService<OrderService>();

                var orderCount = orderService.GetOrderCountForToday();
                _logger.LogInformation("Orders placed today ({Date}): {Count}", DateTime.Today.ToShortDateString(), orderCount);

                // You can add additional logic here, like sending notifications or alerts
                // if orderCount > some threshold, etc.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while monitoring orders.");
            }
        }

        _logger.LogInformation("Order Monitoring Service is stopping.");
    }
}