namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public record CreateCartResponse()
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public DateTime Date { get; init; }
    public List<CreateCartItemResponse> Products { get; init; } = new();
}
public record CreateCartItemResponse(Guid ProductId, string ProductTitle, int Quantity);