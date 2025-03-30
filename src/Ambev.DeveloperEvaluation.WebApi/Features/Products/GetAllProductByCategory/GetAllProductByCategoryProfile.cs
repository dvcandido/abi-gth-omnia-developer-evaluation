using Ambev.DeveloperEvaluation.Application.Products.GetAllProductByCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProductByCategory;

internal class GetAllProductByCategoryProfile : Profile
{
    public GetAllProductByCategoryProfile()
    {
        CreateMap<GetAllProductByCategoryRequest, GetAllProductByCategoryQuery>()
            .ConstructUsing((src, context) => new GetAllProductByCategoryQuery(
                src.PageNumber,
                src.PageSize,
                src.Order,
                (string) context.Items["Category"]
                ));

        CreateMap<GetAllProductByCategoryResult, GetAllProductByCategoryResponse>();
        CreateMap<GetAllProductByCategoryItemResult, GetAllProductByCategoryItemResponse>();
        CreateMap<GetAllProductByCategoryRatingResult, GetAllProductByCategoryRatingResponse>();
    }
}