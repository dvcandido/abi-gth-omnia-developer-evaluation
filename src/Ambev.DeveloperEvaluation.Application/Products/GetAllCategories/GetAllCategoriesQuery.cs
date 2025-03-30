using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllCategories;

public record GetAllCategoriesQuery() : IRequest<IEnumerable<string>>;
