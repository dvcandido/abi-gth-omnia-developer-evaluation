namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;

public record GetAllCartPaginateResponse(IEnumerable<GetAllCartResponse> Data, int TotalItems, int CurrentPage, int TotalPages);

public record GetAllCartResponse(Guid Id, Guid UserId, string UserName, DateTime Date, IEnumerable<GetAllCartItemResponse> Products, decimal Total);

public record GetAllCartItemResponse(Guid ProductId, string ProductTitle, int Quantity, decimal UnitPrice, decimal Discount, decimal TotalPrice);
