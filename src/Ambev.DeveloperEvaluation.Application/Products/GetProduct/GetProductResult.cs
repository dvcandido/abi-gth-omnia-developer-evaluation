namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;
public record GetProductResult(
    Guid Id,
    string Title,
    string Description,
    string Category,
    decimal Price,
    string Image,
    GetRatingResult Rating);

public record GetRatingResult(decimal Rate, int Count);
