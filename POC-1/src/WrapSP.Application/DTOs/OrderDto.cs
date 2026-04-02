namespace WrapSP.Application.DTOs;

public sealed record OrderDto
{
    public int OrderId { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public string ProductName { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public DateTime CreatedAt { get; init; }
}
