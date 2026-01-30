using MassTransit;
using Order.Events;
using Order.Models;
using Order.Repositories;

namespace Order.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        IOrderRepository repository,
        IPublishEndpoint publishEndpoint,
        ILogger<OrderService> logger)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<Models.Order?> GetOrderByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Models.Order>> GetOrdersByUserIdAsync(string userId)
    {
        return await _repository.GetByUserIdAsync(userId);
    }

    public async Task<Models.Order> CreateOrderAsync(Models.Order order)
    {
        // Set initial values
        order.Id = Guid.NewGuid();
        order.Status = OrderStatus.Pending;
        order.CreatedAt = DateTime.UtcNow;

        // Calculate total amount from items
        order.TotalAmount = order.Items.Sum(item => item.Quantity * item.UnitPrice);

        // Save to database
        var createdOrder = await _repository.CreateAsync(order);

        // Publish event
        var orderCreatedEvent = new OrderCreatedEvent
        {
            OrderId = createdOrder.Id,
            UserId = createdOrder.UserId,
            TotalAmount = createdOrder.TotalAmount,
            ItemCount = createdOrder.Items.Count,
            CreatedAt = createdOrder.CreatedAt
        };

        await _publishEndpoint.Publish(orderCreatedEvent);
        _logger.LogInformation("Order {OrderId} created and event published for User {UserId}", 
            createdOrder.Id, createdOrder.UserId);

        return createdOrder;
    }

    public async Task<Models.Order> UpdateOrderStatusAsync(Guid id, OrderStatus status)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {id} not found");
        }

        order.Status = status;
        return await _repository.UpdateAsync(order);
    }

    public async Task<bool> CancelOrderAsync(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null)
        {
            return false;
        }

        // Only allow cancellation if order is still pending
        if (order.Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException($"Cannot cancel order with status {order.Status}");
        }

        order.Status = OrderStatus.Cancelled;
        await _repository.UpdateAsync(order);
        
        _logger.LogInformation("Order {OrderId} cancelled", id);
        return true;
    }
}
