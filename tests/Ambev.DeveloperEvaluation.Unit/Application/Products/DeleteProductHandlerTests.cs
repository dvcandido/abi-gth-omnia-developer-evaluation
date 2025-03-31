using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class DeleteProductHandlerTests
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly DeleteProductHandler _handler;

    public DeleteProductHandlerTests()
    {
        _repository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new DeleteProductHandler(_repository, _mapper);
    }

    [Fact(DisplayName = "Given valid product ID When deleting product Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = new DeleteCartCommand(Guid.NewGuid());
        _repository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(true);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Success.Should().BeTrue();
        await _repository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid product ID When deleting product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var command = new DeleteCartCommand(Guid.Empty);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>().Where(ex => ex.Message.Contains("Product ID is required."));
    }

    /// <summary>
    /// Tests that trying to delete a non-existing product throws a KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existing product ID When deleting product Then throws key not found exception")]
    public async Task Handle_NonExistingProduct_ThrowsKeyNotFoundException()
    {
        // Arrange
        var command = new DeleteCartCommand(Guid.NewGuid());
        _repository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(false);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Product with ID {command.Id} not found");
    }
}
