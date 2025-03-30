using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

internal class GetProductValidator : AbstractValidator<GetProductQuery>
{
    public GetProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product ID is required.");
    }
}
