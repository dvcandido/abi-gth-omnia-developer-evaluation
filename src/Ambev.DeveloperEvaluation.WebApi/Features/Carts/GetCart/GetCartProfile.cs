using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

public class GetCartProfile : Profile
{
    public GetCartProfile()
    {
        CreateMap<GetCartResult, GetCartResponse>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Items));
        CreateMap<GetCartItemResult, GetCartItemResponse>();
    }
}
