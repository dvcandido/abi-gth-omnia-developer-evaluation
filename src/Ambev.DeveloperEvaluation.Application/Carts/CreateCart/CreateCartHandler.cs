using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateCartHandler(ICartRepository cartRepository, IUserRepository userRepository, IProductRepository productRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<CreateCartResult> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequestAsync(request, cancellationToken);

        var user = await GetUserAsync(request.UserId, cancellationToken);

        var products = await GetProductsAsync(request.Items, cancellationToken);

        var cart = new Cart(request.UserId, request.Date);

        cart.SetUserInfo(request.UserName);

        foreach (var item in request.Items)
        {
            var product = products[item.ProductId];

            cart.AddItem(product.Id, product.Title, item.Quantity, product.Price);
        }

        var result = await _cartRepository.AddAsync(cart, cancellationToken);

        return _mapper.Map<CreateCartResult>(result);
    }

    private async Task ValidateRequestAsync(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }

    private async Task<User> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            throw new KeyNotFoundException($"User with ID {userId} not found");
        return user;
    }

    private async Task<Dictionary<Guid, Product>> GetProductsAsync(IEnumerable<CreateCartItemCommand> items, CancellationToken cancellationToken)
    {
        var products = new Dictionary<Guid, Product>();
        foreach (var item in items)
        {
            var productEntity = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
            if (productEntity is null)
                throw new KeyNotFoundException($"Product with ID {item.ProductId} not found");
            products[item.ProductId] = productEntity;
        }
        return products;
    }
}
