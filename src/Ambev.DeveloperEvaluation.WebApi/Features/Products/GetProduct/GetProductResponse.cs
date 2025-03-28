namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

public record GetProductResponse(
    Guid Id,
    string Title,
    string Description,
    string Category,
    decimal Price,
    string Image,
    GetRatingResponse Rating);

public record GetRatingResponse(decimal Rate, int Count);