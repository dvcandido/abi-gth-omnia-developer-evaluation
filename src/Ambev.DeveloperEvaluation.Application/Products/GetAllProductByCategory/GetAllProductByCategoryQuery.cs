using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProductByCategory;

public record GetAllProductByCategoryQuery(int PageNumber, int PageSize, string Order, string Category) : IRequest<GetAllProductByCategoryResult>;
