using Order.Models;

namespace Order.DTOs;

public class UpdateOrderStatusRequest
{
    public OrderStatus Status { get; set; }
}
