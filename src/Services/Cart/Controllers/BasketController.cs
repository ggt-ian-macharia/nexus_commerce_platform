using AutoMapper;
using Cart.DTOs;
using Cart.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cart.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;
    private readonly IMapper _mapper;
    private readonly ILogger<BasketController> _logger;

    public BasketController(
        IBasketService basketService,
        IMapper mapper,
        ILogger<BasketController> logger)
    {
        _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("{userName}", Name = "GetBasket")]
    [ProducesResponseType(typeof(BasketResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BasketResponse>> GetBasket(string userName)
    {
        var basket = await _basketService.GetBasketAsync(userName);
        var response = _mapper.Map<BasketResponse>(basket);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BasketResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BasketResponse>> UpdateBasket([FromBody] UpdateBasketRequest request)
    {
        var basket = await _basketService.UpdateBasketAsync(request);
        var response = _mapper.Map<BasketResponse>(basket);
        return Ok(response);
    }

    [HttpDelete("{userName}")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
        await _basketService.DeleteBasketAsync(userName);
        return Ok();
    }
}
