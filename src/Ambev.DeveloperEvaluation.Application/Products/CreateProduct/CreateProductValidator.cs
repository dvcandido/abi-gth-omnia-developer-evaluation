using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Category).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Image).NotEmpty();
        RuleFor(x => x.Rating).NotNull().SetValidator(new RatingCommandValidator());
    }

    private class RatingCommandValidator : AbstractValidator<CreateRatingCommand>
    {
        public RatingCommandValidator()
        {
            RuleFor(r => r.Rate).InclusiveBetween(0, 5);
            RuleFor(r => r.Count).GreaterThanOrEqualTo(0);
        }
    }
}
