namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;

public record GetAllProductResult(IEnumerable<GetAllProductItemResult> Data, int TotalItems, int CurrentPage, int TotalPages);
public record GetAllProductItemResult(
    Guid Id,
    string Title,
    string Description,
    string Category,
    decimal Price,
    string Image,
    GetAllProductRatingResult Rating);

public record GetAllProductRatingResult(decimal Rate, int Count);
