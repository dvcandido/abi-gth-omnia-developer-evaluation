using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public record CreateProductCommand(
    string Title,
    string Description,
    string Category,
    decimal Price,
    string Image,
    CreateRatingCommand Rating) : IRequest<CreateProductResult>;

public record CreateRatingCommand(decimal Rate, int Count);