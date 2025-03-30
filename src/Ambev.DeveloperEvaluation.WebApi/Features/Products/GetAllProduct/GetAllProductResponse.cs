namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProduct;

public record GetAllProductResponse(IEnumerable<GetAllProductItemResponse> Data, int TotalItems, int CurrentPage, int TotalPages);
public record GetAllProductItemResponse(
    Guid Id,
    string Title,
    string Description,
    string Category,
    decimal Price,
    string Image,
    GetAllProductRatingResponse Rating);

public record GetAllProductRatingResponse(decimal Rate, int Count);