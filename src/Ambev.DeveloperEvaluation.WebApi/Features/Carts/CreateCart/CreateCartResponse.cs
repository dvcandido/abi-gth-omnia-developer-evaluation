namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public record CreateCartResponse()
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public DateTime Date { get; init; }
    public IEnumerable<CreateCartItemResponse> Products { get; init; } = [];
    public decimal Total { get; init; }
}
public record CreateCartItemResponse(Guid ProductId, string ProductTitle, int Quantity,decimal UnitPrice, decimal Discount, decimal TotalPrice);