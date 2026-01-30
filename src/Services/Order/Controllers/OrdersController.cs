using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Order.DTOs;
using Order.Models;
using Order.Services;

namespace Order.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService orderService, IMapper mapper, ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResponse>> GetOrderById(Guid id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound(new { Message = $"Order with ID {id} not found" });
        }

        var response = _mapper.Map<OrderResponse>(order);
        return Ok(response);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByUserId(string userId)
    {
        var orders = await _orderService.GetOrdersByUserIdAsync(userId);
        var response = _mapper.Map<IEnumerable<OrderResponse>>(orders);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponse>> CreateOrder(CreateOrderRequest request)
    {
        try
        {
            var order = _mapper.Map<Models.Order>(request);
            
            // Set unique IDs for order items
            foreach (var item in order.Items)
            {
                item.Id = Guid.NewGuid();
            }

            var createdOrder = await _orderService.CreateOrderAsync(order);
            var response = _mapper.Map<OrderResponse>(createdOrder);
            
            return CreatedAtAction(nameof(GetOrderById), new { id = response.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order for user {UserId}", request.UserId);
            return StatusCode(500, new { Message = "An error occurred while creating the order" });
        }
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<ActionResult<OrderResponse>> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusRequest request)
    {
        try
        {
            var updatedOrder = await _orderService.UpdateOrderStatusAsync(id, request.Status);
            var response = _mapper.Map<OrderResponse>(updatedOrder);
            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { Message = $"Order with ID {id} not found" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating order {OrderId} status", id);
            return StatusCode(500, new { Message = "An error occurred while updating the order status" });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        try
        {
            var result = await _orderService.CancelOrderAsync(id);
            if (!result)
            {
                return NotFound(new { Message = $"Order with ID {id} not found" });
            }

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cancelling order {OrderId}", id);
            return StatusCode(500, new { Message = "An error occurred while cancelling the order" });
        }
    }
}
