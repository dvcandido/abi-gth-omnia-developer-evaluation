using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct
{
    internal class CreateProductProfile : Profile
    {
        public CreateProductProfile()
        {
            CreateMap<Product, GetAllProductItemResult>();
            CreateMap<Rating, GetAllProductRatingResult>();
        }
    }
}
