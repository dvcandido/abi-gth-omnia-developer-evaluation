namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

public record CreateCartResult(Guid Id, Guid UserId, string UserName, DateTime Date, List<CreateCartItemResult> Items);
public record CreateCartItemResult(Guid ProductId, string ProductTitle, int Quantity);