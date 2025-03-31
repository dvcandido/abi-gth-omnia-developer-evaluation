namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

public record GetCartResponse(
    Guid Id,
    Guid UserId,
    string UserName,
    DateTime Date,
    List<GetCartItemResponse> Items);

public record GetCartItemResponse(Guid ProductId, string ProductTitle, int Quantity);
