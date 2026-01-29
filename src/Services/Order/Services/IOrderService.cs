namespace Order.Services;

public interface IOrderService
{
    Task<Models.Order?> GetOrderByIdAsync(Guid id);
    Task<IEnumerable<Models.Order>> GetOrdersByUserIdAsync(string userId);
    Task<Models.Order> CreateOrderAsync(Models.Order order);
    Task<Models.Order> UpdateOrderStatusAsync(Guid id, Models.OrderStatus status);
    Task<bool> CancelOrderAsync(Guid id);
}
