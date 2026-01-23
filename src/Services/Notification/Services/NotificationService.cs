namespace Notification.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    public async Task SendProductCreatedNotificationAsync(int productId, string productName, decimal price)
    {
        // Simulate sending notification (email, SMS, push, etc.)
        _logger.LogInformation(
            "📧 NOTIFICATION: New product created! ID: {ProductId}, Name: '{ProductName}', Price: ${Price:F2}",
            productId, productName, price);

        // Simulate async operation
        await Task.Delay(100);

        _logger.LogInformation("✅ Notification sent successfully for product {ProductId}", productId);
    }
}
