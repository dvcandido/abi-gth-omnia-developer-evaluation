using Ambev.DeveloperEvaluation.Application.Products.GetAllCategories;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class GetAllCategoriesHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly GetAllCategoriesHandler _handler;

    public GetAllCategoriesHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new GetAllCategoriesHandler(_productRepository);
    }

    [Fact(DisplayName = "Given request to get all categories When handling Then returns category list")]
    public async Task Handle_Request_ReturnsCategoryList()
    {
        // Arrange
        var categories = new List<string> { "Bebidas", "Comidas", "Snacks" };
        _productRepository.GetCategoriesAsync(Arg.Any<CancellationToken>())
            .Returns(categories);

        var query = new GetAllCategoriesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(categories);
        await _productRepository.Received(1).GetCategoriesAsync(Arg.Any<CancellationToken>());
    }
}
