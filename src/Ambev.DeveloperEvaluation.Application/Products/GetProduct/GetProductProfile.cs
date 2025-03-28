using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
