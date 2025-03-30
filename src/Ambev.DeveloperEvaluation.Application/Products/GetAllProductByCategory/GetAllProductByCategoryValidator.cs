using FluentValidation;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProductByCategory;

internal class GetAllProductByCategoryValidator : AbstractValidator<GetAllProductByCategoryQuery>
{
    public GetAllProductByCategoryValidator()
    {
        RuleFor(x => x.PageNumber)
           .GreaterThan(0).WithMessage("Page must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Size must be less than or equal to 100.");

        RuleFor(x => x.Order)
            .Must(BeValidOrder).WithMessage("Applied order is invalid.");

        RuleFor(RuleFor => RuleFor.Category)
            .NotEmpty().WithMessage("Category must be informed.");
    }

    private bool BeValidOrder(string order)
    {
        if (string.IsNullOrWhiteSpace(order))
        {
            return true;
        }

        string pattern = @"^\s*[a-zA-Z]+\s+(asc|desc)(\s*,\s*[a-zA-Z]+\s+(asc|desc))*\s*$";
        return Regex.IsMatch(order, pattern, RegexOptions.IgnoreCase);
    }
}