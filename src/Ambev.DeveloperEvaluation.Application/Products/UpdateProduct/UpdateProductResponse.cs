namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public record UpdateProductResponse(
    Guid Id,
    string Title,
    string Description,
    string Category,
    decimal Price,
    string Image,
    UpdateRatingResponse Rating);

public record UpdateRatingResponse(decimal Rate, int Count);