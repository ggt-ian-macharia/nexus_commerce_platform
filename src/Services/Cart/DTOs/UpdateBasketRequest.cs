namespace Cart.DTOs;

public class UpdateBasketRequest
{
    public string UserName { get; set; } = string.Empty;
    public List<ShoppingCartItemDto> Items { get; set; } = new();
}

public class ShoppingCartItemDto
{
    public int Quantity { get; set; }
    public string Color { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
}
