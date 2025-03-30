using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;

public record GetAllProductQuery(int PageNumber, int PageSize, string Order) : IRequest<GetAllProductResult>;
