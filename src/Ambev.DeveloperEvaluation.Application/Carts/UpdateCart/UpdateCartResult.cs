namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

public record UpdateCartResult(Guid Id, Guid UserId, string UserName, DateTime Date, List<UpdateCartItemResult> Items, decimal Total);

public record UpdateCartItemResult(Guid ProductId, string ProductTitle, int Quantity, decimal UnitPrice, decimal Discount, decimal TotalPrice);