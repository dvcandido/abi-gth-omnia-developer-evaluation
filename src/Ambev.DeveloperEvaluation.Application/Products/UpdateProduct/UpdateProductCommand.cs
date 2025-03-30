using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Title,
    string Description,
    string Category,
    decimal Price,
    string Image,
    UpdateRatingCommand Rating) : IRequest<UpdateProductResponse>;

public record UpdateRatingCommand(decimal Rate, int Count);