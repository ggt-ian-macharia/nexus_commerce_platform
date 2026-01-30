namespace Order.DTOs;

public class CreateOrderRequest
{
    public string UserId { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public string ShippingCity { get; set; } = string.Empty;
    public string ShippingZipCode { get; set; } = string.Empty;
    public string ShippingCountry { get; set; } = string.Empty;
    public List<CreateOrderItemRequest> Items { get; set; } = new();
}
