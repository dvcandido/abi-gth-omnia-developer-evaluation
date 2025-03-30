using Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProduct
{
    public class GetAllProductProfile : Profile
    {
        public GetAllProductProfile()
        {
            CreateMap<GetAllProductRequest, GetAllProductQuery>();

            CreateMap<GetAllProductResult, GetAllProductResponse>();
            CreateMap<GetAllProductItemResult, GetAllProductItemResponse>();
            CreateMap<GetAllProductRatingResult, GetAllProductRatingResponse>();
        }
    }
}
