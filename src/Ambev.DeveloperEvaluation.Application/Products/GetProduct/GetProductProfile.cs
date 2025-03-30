using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    internal class CreateProductProfile : Profile
    {
        public CreateProductProfile()
        {
            CreateMap<Product, GetProductResult>();
            CreateMap<Rating, GetRatingResult>();
        }
    }
}
