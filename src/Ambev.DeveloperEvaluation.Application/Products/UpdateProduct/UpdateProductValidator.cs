using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Category).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Image).NotEmpty();
        RuleFor(x => x.Rating).NotNull().SetValidator(new RatingCommandValidator());
    }

    private class RatingCommandValidator : AbstractValidator<UpdateRatingCommand>
    {
        public RatingCommandValidator()
        {
            RuleFor(r => r.Rate).InclusiveBetween(0, 5);
            RuleFor(r => r.Count).GreaterThanOrEqualTo(0);
        }
    }
}

