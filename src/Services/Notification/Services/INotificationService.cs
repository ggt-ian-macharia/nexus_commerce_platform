namespace Notification.Services;

public interface INotificationService
{
    Task SendProductCreatedNotificationAsync(int productId, string productName, decimal price);
}
