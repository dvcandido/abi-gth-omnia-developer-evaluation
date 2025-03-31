namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public record UpdateCartRequest(
    Guid UserId,
    string UserName,
    DateTime Date,
    List<UpdateCartItemRequest> Products
);

public record UpdateCartItemRequest(Guid ProductId, string ProductTitle, int Quantity);
