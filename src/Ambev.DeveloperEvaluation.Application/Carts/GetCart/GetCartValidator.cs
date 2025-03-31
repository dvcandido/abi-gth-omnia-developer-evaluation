using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

internal class GetCartValidator : AbstractValidator<GetCartQuery>
{
    public GetCartValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Cart ID is required.");
    }
}