namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

public class GetCartResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public DateTime Date { get; init; }
    public IEnumerable<GetCartItemResponse> Products { get; init; } = [];
    public decimal Total { get; init; }
}
public record GetCartItemResponse(Guid ProductId, string ProductTitle, int Quantity, decimal UnitPrice, decimal Discount, decimal TotalPrice);