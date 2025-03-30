using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductProfile : Profile
{
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductRequest, UpdateProductCommand>()
            .ConstructUsing((src, context) => new UpdateProductCommand(
                    (Guid)context.Items["ProductId"],
                    src.Title,
                    src.Description,
                    src.Category,
                    src.Price,
                    src.Image,
                    new UpdateRatingCommand(src.Rating.Rate, src.Rating.Count)));

        CreateMap<UpdateRatingRequest, UpdateRatingCommand>();
    }
}
