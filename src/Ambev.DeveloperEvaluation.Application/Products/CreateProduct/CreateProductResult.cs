namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public record CreateProductResult(
    Guid Id,
    string Title,
    string Description,
    string Category,
    decimal Price,
    string Image,
    CreateRatingResult Rating);

public record CreateRatingResult(decimal Rate, int Count);