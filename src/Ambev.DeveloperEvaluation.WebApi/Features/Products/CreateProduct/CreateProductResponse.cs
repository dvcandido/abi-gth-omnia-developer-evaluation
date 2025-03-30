namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

internal record CreateProductResponse(
    Guid Id,
    string Title,
    string Description,
    string Category,
    decimal Price,
    string Image,
    CreateRatingResponse Rating);

internal record CreateRatingResponse(decimal Rate, int Count);