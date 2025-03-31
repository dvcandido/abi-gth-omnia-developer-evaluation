using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CartValidator : AbstractValidator<Cart>
{
    public CartValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(c => c.UserName)
            .NotEmpty().WithMessage("User name is required.")
            .MaximumLength(100).WithMessage("User name must not exceed 100 characters.");

        RuleFor(c => c.Date)
            .NotEmpty().WithMessage("Cart date is required.");

        RuleForEach(c => c.Items)
            .SetValidator(new CartItemValidator());
    }
}
