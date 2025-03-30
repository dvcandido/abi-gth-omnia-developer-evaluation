namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public record CreateProductRequest(
    string Title,
    string Description,
    string Category,
    decimal Price,
    string Image,
    CreateRatingRequest Rating);

public record CreateRatingRequest(decimal Rate, int Count);

