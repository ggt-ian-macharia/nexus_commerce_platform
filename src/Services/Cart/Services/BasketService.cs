using AutoMapper;
using Cart.Data;
using Cart.DTOs;
using Cart.Models;

namespace Cart.Services;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<BasketService> _logger;

    public BasketService(
        IBasketRepository repository,
        IMapper mapper,
        ILogger<BasketService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ShoppingCart?> GetBasketAsync(string userName)
    {
        var basket = await _repository.GetBasketAsync(userName);
        
        if (basket == null)
        {
            _logger.LogInformation("Basket for user {UserName} not found, returning empty basket", userName);
            return new ShoppingCart(userName);
        }

        return basket;
    }

    public async Task<ShoppingCart> UpdateBasketAsync(UpdateBasketRequest request)
    {
        var basket = _mapper.Map<ShoppingCart>(request);
        
        var updatedBasket = await _repository.UpdateBasketAsync(basket);
        
        _logger.LogInformation("Basket updated for user {UserName} with {ItemCount} items", 
            updatedBasket.UserName, updatedBasket.Items.Count);
        
        return updatedBasket;
    }

    public async Task DeleteBasketAsync(string userName)
    {
        await _repository.DeleteBasketAsync(userName);
        _logger.LogInformation("Basket deleted for user {UserName}", userName);
    }
}
