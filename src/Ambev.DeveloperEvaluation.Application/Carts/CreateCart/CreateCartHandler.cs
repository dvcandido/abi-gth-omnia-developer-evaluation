using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;

    public CreateCartHandler(ICartRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreateCartResult> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {

        var validator = new CreateCartValidator();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cart = new Cart(request.UserId, request.Date);

        cart.SetUserInfo(request.UserName);

        foreach (var product in request.Items)
        {
            cart.AddItem(product.ProductId, product.ProductTitle, product.Quantity);
        }

        var result = await _repository.AddAsync(cart, cancellationToken);

        return _mapper.Map<CreateCartResult>(result);
    }
}
