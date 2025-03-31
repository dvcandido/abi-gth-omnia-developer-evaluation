namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public record CreateCartRequest(Guid UserId, string UserName, DateTime Date, IEnumerable<CreateCartItemRequest> Products);
public record CreateCartItemRequest(Guid ProductId, int Quantity);
