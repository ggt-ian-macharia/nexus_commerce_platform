using EventBus.Events;
using MassTransit;
using Notification.Services;

namespace Notification.Consumers;

public class ProductCreatedEventConsumer : IConsumer<ProductCreatedEvent>
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<ProductCreatedEventConsumer> _logger;

    public ProductCreatedEventConsumer(
        INotificationService notificationService,
        ILogger<ProductCreatedEventConsumer> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation(
            "Received ProductCreatedEvent: ID={Id}, Name='{Name}', Price={Price}",
            message.Id, message.Name, message.Price);

        try
        {
            await _notificationService.SendProductCreatedNotificationAsync(
                message.Id,
                message.Name,
                message.Price);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing ProductCreatedEvent for product {ProductId}", message.Id);
            throw;
        }
    }
}
