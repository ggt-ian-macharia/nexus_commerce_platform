using Cart.DTOs;
using Cart.Models;

namespace Cart.Services;

public interface IBasketService
{
    Task<ShoppingCart?> GetBasketAsync(string userName);
    Task<ShoppingCart> UpdateBasketAsync(UpdateBasketRequest request);
    Task DeleteBasketAsync(string userName);
}
