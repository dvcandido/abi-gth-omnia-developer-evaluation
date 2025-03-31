using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class CreateProductHandlerTests
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly CreateProductHandler _handler;

    public CreateProductHandlerTests()
    {
        _repository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateProductHandler(_repository, _mapper);
    }

    [Fact(DisplayName = "Given valid product data When creating product Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = CreateProductHandlerTestData.GenerateValidCommand();

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
            Category = command.Category,
            Price = command.Price,
            Image = command.Image
        };

        var result = new CreateProductResult(
            product.Id,
            product.Title,
            product.Description,
            product.Category,
            product.Price,
            product.Image,
            new CreateRatingResult(command.Rating.Rate, command.Rating.Count)
        );

        _mapper.Map<Product>(command).Returns(product);
        _mapper.Map<CreateProductResult>(product).Returns(result);

        _repository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(product);

        // Act
        var createProductResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        createProductResult.Should().NotBeNull();
        createProductResult.Id.Should().Be(product.Id);
        await _repository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid product data When creating product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateProductCommand("", "", "", -10.0m, "", null);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given valid command When handling Then maps command to product entity")]
    public async Task Handle_ValidRequest_MapsCommandToProduct()
    {
        // Arrange
        var command = CreateProductHandlerTestData.GenerateValidCommand();

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
            Category = command.Category,
            Price = command.Price,
            Image = command.Image
        };

        _mapper.Map<Product>(command).Returns(product);
        _repository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(product);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapper.Received(1).Map<Product>(Arg.Is<CreateProductCommand>(c =>
            c.Title == command.Title &&
            c.Description == command.Description &&
            c.Category == command.Category &&
            c.Price == command.Price &&
            c.Image == command.Image));
    }
}
