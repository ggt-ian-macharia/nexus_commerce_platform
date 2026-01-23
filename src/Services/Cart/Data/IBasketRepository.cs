using Cart.Models;

namespace Cart.Data;

public interface IBasketRepository
{
    Task<ShoppingCart?> GetBasketAsync(string userName);
    Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket);
    Task DeleteBasketAsync(string userName);
}
