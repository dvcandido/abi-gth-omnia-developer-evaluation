namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public record UpdateCartResponse()
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public DateTime Date { get; init; }
    public IEnumerable<UpdateCartItemResponse> Products { get; init; } = [];
    public decimal Total { get; init; }
}

public record UpdateCartItemResponse(Guid ProductId, string ProductTitle, int Quantity, decimal UnitPrice, decimal Discount, decimal TotalPrice);
