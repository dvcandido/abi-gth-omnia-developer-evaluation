using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;

public class GetAllProductProfile : Profile
{
    public GetAllProductProfile()
    {
        CreateMap<GetAllCartsRequest, GetAllCartsQuery>();

        CreateMap<GetAllCartPaginateResult, GetAllCartPaginateResponse>();
        CreateMap<GetAllCartResult, GetAllCartResponse>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Items)); 
        CreateMap<GetAllCartItemResult, GetAllCartItemResponse>();
    }
}