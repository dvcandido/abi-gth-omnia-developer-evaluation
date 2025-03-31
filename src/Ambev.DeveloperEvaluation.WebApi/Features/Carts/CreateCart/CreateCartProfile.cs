using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public class CreateCartProfile : Profile
{
    public CreateCartProfile()
    {
        CreateMap<CreateCartRequest, CreateCartCommand>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Products));
        CreateMap<CreateCartItemRequest, CreateCartItemCommand>();

        CreateMap<CreateCartResult, CreateCartResponse>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Items));
        CreateMap<CreateCartItemResult, CreateCartItemResponse>();
    }
}