using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public class UpdateCartProfile : Profile
{
    public UpdateCartProfile()
    {
        CreateMap<UpdateCartRequest, UpdateCartCommand>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Products));
        CreateMap<UpdateCartItemRequest, UpdateCartItemCommand>();

        CreateMap<UpdateCartResult, UpdateCartResponse>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Items));
        CreateMap<UpdateCartItemResult, UpdateCartItemResponse>();
    }
}