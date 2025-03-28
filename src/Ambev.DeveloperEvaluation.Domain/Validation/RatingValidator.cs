using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class RatingValidator : AbstractValidator<Rating>
    {
        public RatingValidator()
        {
            RuleFor(rate => rate.Rate)
                .InclusiveBetween(0, 5).WithMessage("Rate must be between 0 and 5.");

            RuleFor(rate => rate.Count)
                .GreaterThanOrEqualTo(0).WithMessage("Count cannot be negative.");
        }
    }
}
