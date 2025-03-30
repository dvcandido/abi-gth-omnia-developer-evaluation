using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;

public class GetallProductHandler : IRequestHandler<GetAllProductQuery, GetAllProductResult>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetallProductHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetAllProductResult> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetAllProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var (products, totalItems) = await _repository.GetAllAsync(request.PageNumber, request.PageSize, request.Order, cancellationToken);

        var totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);
        var currentPage = request.PageNumber;
        var data = _mapper.Map<IEnumerable<GetAllProductItemResult>>(products);

        return new GetAllProductResult(data, totalItems, currentPage, totalPages);
    }
}
