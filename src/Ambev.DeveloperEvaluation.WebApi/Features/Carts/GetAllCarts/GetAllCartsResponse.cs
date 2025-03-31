namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;

public record GetAllCartPaginateResponse(
    IEnumerable<GetAllCartResponse> Data,
    int TotalItems,
    int CurrentPage,
    int TotalPages
);

public record GetAllCartResponse(
    Guid Id,
    Guid UserId,
    string UserName,
    DateTime Date,
    List<GetAllCartItemResponse> Items
);

public record GetAllCartItemResponse(
    Guid ProductId,
    string ProductTitle,
    int Quantity
);
