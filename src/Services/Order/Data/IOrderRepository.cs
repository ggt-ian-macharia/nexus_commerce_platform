namespace Order.Data;

public interface IOrderRepository
{
    Task<Models.Order?> GetByIdAsync(Guid id);
    Task<IEnumerable<Models.Order>> GetByUserIdAsync(string userId);
    Task<Models.Order> CreateAsync(Models.Order order);
    Task<Models.Order> UpdateAsync(Models.Order order);
    Task<bool> DeleteAsync(Guid id);
}
