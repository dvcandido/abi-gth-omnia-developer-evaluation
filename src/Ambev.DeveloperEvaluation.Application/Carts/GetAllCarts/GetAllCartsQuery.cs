using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

public record GetAllCartsQuery(int PageNumber = 1, int PageSize = 10, string Order = "") : IRequest<GetAllCartPaginateResult>;
