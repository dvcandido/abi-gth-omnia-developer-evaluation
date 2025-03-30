using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Title)
            .NotEmpty()
            .Length(3, 100).WithMessage("Title must be between 3 and 100 characters long.");

            RuleFor(product => product.Description)
                .NotEmpty()
                .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");

            RuleFor(p => p.Category)
            .NotEmpty()
            .MaximumLength(50).WithMessage("Category cannot exceed 50 characters.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(p => p.Image)
                .NotEmpty()
                .Must(BeAValidUrl).WithMessage("Image must be a vaid URL.");

            RuleFor(p => p.Rating)
                .NotNull().WithMessage("Rating is required.")
                .SetValidator(new RatingValidator());

        }

        private bool BeAValidUrl(string imageUrl)
        {
            return Uri.TryCreate(imageUrl, UriKind.Absolute, out var uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
