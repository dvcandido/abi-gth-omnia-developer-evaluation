using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProductByCategory;

public class GetAllProductByCategoryHandler : IRequestHandler<GetAllProductByCategoryQuery, GetAllProductByCategoryResult>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetAllProductByCategoryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetAllProductByCategoryResult> Handle(GetAllProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetAllProductByCategoryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var (products, totalItems) = await _repository.GetAllByCategoryAsync(request.Category, request.PageNumber, request.PageSize, request.Order, cancellationToken);

        var totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);
        var currentPage = request.PageNumber;
        var data = _mapper.Map<IEnumerable<GetAllProductByCategoryItemResult>>(products);

        return new GetAllProductByCategoryResult(data, totalItems, currentPage, totalPages);
    }
}
