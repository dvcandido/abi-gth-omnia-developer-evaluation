using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

internal class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Product ID must be a valid GUID.");
    }
}
