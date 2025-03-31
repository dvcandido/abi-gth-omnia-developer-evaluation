namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

public record GetAllCartPaginateResult(
    IEnumerable<GetAllCartResult> Data,
    int TotalItems,
    int CurrentPage,
    int TotalPages
);

public record GetAllCartResult(
    Guid Id,
    Guid UserId,
    string UserName,
    DateTime Date,
    List<GetAllCartItemResult> Items
);

public record GetAllCartItemResult(
    Guid ProductId,
    string ProductTitle,
    int Quantity
);
