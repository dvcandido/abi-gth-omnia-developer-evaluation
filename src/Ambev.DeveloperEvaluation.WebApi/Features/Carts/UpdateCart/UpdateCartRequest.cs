namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public record UpdateCartRequest(Guid UserId, string UserName, DateTime Date, IEnumerable<UpdateCartItemRequest> Products);

public record UpdateCartItemRequest(Guid ProductId, int Quantity);
