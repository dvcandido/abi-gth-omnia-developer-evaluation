using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using MediatR;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateCartHandler(ICartRepository cartRepository, IUserRepository userRepository, IProductRepository productRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<UpdateCartResult> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {

        await ValidateRequestAsync(request, cancellationToken);

        var user = await GetUserAsync(request.UserId, cancellationToken);
        var products = await GetProductsAsync(request.Items, cancellationToken);

        var cart = new Cart(request.UserId, request.Date)
        {
            Id = request.Id
        };

        cart.SetUserInfo(request.UserName);
        foreach (var item in request.Items)
        {
            var product = products[item.ProductId];

            cart.UpdateItem(product.Id, product.Title, item.Quantity, product.Price);
        }

        var updatedCart = await _cartRepository.UpdateAsync(cart, cancellationToken);

        if (updatedCart == null)
            throw new KeyNotFoundException($"Cart with ID {request.Id} not found");

        return _mapper.Map<UpdateCartResult>(updatedCart);
    }

    private async Task ValidateRequestAsync(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateCartValidator();
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

    private async Task<Dictionary<Guid, Product>> GetProductsAsync(IEnumerable<UpdateCartItemCommand> items, CancellationToken cancellationToken)
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
