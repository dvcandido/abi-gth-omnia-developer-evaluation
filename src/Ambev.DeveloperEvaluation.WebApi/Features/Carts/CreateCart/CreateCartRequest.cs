namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public record CreateCartRequest(Guid UserId, string UserName, DateTime Date, List<CreateCartItemRequest> Products);
public record CreateCartItemRequest(Guid ProductId, string ProductTitle, int Quantity);
