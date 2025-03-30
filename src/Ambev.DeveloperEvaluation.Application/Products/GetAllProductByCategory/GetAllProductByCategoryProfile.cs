using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProductByCategory
{
    public class GetAllProductByCategoryProfile : Profile
    {
        public GetAllProductByCategoryProfile()
        {
            CreateMap<Product, GetAllProductByCategoryItemResult>();
            CreateMap<Rating, GetAllProductByCategoryRatingResult>();
        }
    }
}
