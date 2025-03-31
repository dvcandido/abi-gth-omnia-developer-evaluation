using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

public class GetAllCartsHandler : IRequestHandler<GetAllCartsQuery, GetAllCartPaginateResult>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCartsHandler(ICartRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetAllCartPaginateResult> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetAllCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var (carts, totalItems) = await _repository.GetAllAsync(request.PageNumber, request.PageSize, request.Order, cancellationToken);
        
        var totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);
        var currentPage = request.PageNumber;    
        
        var data = _mapper.Map<IEnumerable<GetAllCartResult>>(carts);

        return new GetAllCartPaginateResult(data, totalItems, currentPage, totalPages);
    }
}
