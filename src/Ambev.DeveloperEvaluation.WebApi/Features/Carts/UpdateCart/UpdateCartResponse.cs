namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public record UpdateCartResponse()
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public DateTime Date { get; init; }
    public List<UpdateCartItemResponse> Products { get; init; } = [];
}

public record UpdateCartItemResponse(Guid ProductId, string ProductTitle, int Quantity);
