using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllCategories;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<string>>
{
    private readonly IProductRepository _productRepository;

    public GetAllCategoriesHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<string>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetCategoriesAsync(cancellationToken);
    }
}