namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProductByCategory;

public record GetAllProductByCategoryResponse(IEnumerable<GetAllProductByCategoryItemResponse> Data, int TotalItems, int CurrentPage, int TotalPages);

public record GetAllProductByCategoryItemResponse(
    Guid Id,
    string Title,
    string Description,
    string Category,
    decimal Price,
    string Image,
    GetAllProductByCategoryRatingResponse Rating);

public record GetAllProductByCategoryRatingResponse(decimal Rate, int Count);