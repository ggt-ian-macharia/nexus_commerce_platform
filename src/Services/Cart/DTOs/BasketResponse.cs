namespace Cart.DTOs;

public class BasketResponse
{
    public string UserName { get; set; } = string.Empty;
    public List<BasketItemResponse> Items { get; set; } = new();
    public decimal TotalPrice { get; set; }
}

public class BasketItemResponse
{
    public int Quantity { get; set; }
    public string Color { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal ItemTotal { get; set; }
}
