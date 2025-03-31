using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class UpdateProductHandlerTests
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly UpdateProductHandler _handler;

    public UpdateProductHandlerTests()
    {
        _repository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateProductHandler(_repository, _mapper);
    }

    [Fact(DisplayName = "Given valid product data When updating product Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = UpdateProductHandlerTestData.GenerateValidCommand();

        var product = new Product
        {
            Id = command.Id,
            Title = command.Title,
            Description = command.Description,
            Category = command.Category,
            Price = command.Price,
            Image = command.Image,
            Rating = new Rating { Rate = command.Rating.Rate, Count = command.Rating.Count }
        };

        var response = new UpdateProductResponse(
            product.Id, product.Title, product.Description, product.Category,
            product.Price, product.Image, new UpdateRatingResponse(product.Rating.Rate, product.Rating.Count)
        );

        _mapper.Map<Product>(command).Returns(product);
        _repository.UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns(product);
        _mapper.Map<UpdateProductResponse>(product).Returns(response);

        // Act
        var updateProductResponse = await _handler.Handle(command, CancellationToken.None);

        // Assert
        updateProductResponse.Should().NotBeNull();
        updateProductResponse.Id.Should().Be(product.Id);
        await _repository.Received(1).UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid product data When updating product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var command = new UpdateProductCommand(Guid.Empty, "", "", "", 0m, "", null);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given non-existent product ID When updating product Then throws KeyNotFoundException")]
    public async Task Handle_ProductNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var command = UpdateProductHandlerTestData.GenerateValidCommand();

        _mapper.Map<Product>(command).Returns(new Product());
        _repository.UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns((Product)null);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Product with ID {command.Id} not found");
    }
}