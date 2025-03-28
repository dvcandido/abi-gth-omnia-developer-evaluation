using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductProfile : Profile
    {
        public UpdateProductProfile()
        {
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<UpdateRatingCommand, Rating>();

            CreateMap<Product, UpdateProductResponse>();
            CreateMap<Rating, UpdateRatingResponse>();
        }
    }
}
