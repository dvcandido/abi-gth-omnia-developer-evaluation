namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProductByCategory;

public record GetAllProductByCategoryResult(IEnumerable<GetAllProductByCategoryItemResult> Data, int TotalItems, int CurrentPage, int TotalPages);

public record GetAllProductByCategoryItemResult(
    Guid Id,
    string Title,
    string Description,
    string Category,
    decimal Price,
    string Image,
    GetAllProductByCategoryRatingResult Rating);

public record GetAllProductByCategoryRatingResult(decimal Rate, int Count);