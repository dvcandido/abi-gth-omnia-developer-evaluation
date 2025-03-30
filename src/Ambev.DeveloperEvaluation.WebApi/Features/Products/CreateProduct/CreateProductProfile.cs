using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

internal class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<CreateRatingRequest, CreateRatingCommand>();

        CreateMap<CreateProductResult, CreateProductResponse>();
        CreateMap<CreateRatingResult, CreateRatingResponse>();
    }
}
