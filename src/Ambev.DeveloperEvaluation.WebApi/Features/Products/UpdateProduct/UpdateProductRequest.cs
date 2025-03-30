namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public record UpdateProductRequest(
    string Title,
    string Description,
    string Category,
    decimal Price,
    string Image,
    UpdateRatingRequest Rating);

public record UpdateRatingRequest(decimal Rate, int Count);