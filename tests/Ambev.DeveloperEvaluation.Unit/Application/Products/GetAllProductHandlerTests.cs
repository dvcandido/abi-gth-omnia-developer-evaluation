using Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class GetAllProductHandlerTests
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly GetallProductHandler _handler;

    public GetAllProductHandlerTests()
    {
        _repository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetallProductHandler(_repository, _mapper);
    }

    [Fact(DisplayName = "Given valid query When getting products Then returns paginated product response")]
    public async Task Handle_ValidQuery_ReturnsPaginatedResponse()
    {
        // Arrange
        var query = new GetAllProductQuery(PageNumber: 1, PageSize: 10, Order: "title asc");

        var products = new List<Product>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Product 1",
                Description = "Description 1",
                Category = "Category 1",
                Price = 10.5m,
                Image = "image.jpg",
                Rating = new Rating(4.5m, 10)
            }
        };

        var productResults = new List<GetAllProductItemResult>
        {
            new(
                Id: products[0].Id,
                Title: products[0].Title,
                Description: products[0].Description,
                Category: products[0].Category,
                Price: products[0].Price,
                Image: products[0].Image,
                Rating: new GetAllProductRatingResult(4.5m, 10))
        };

        _repository.GetAllAsync(query.PageNumber, query.PageSize, query.Order, Arg.Any<CancellationToken>())
            .Returns((products, 1));

        _mapper.Map<IEnumerable<GetAllProductItemResult>>(products).Returns(productResults);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.TotalItems.Should().Be(1);
        result.CurrentPage.Should().Be(1);
        result.TotalPages.Should().Be(1);
    }

    [Fact(DisplayName = "Given invalid query When getting products Then throws validation exception")]
    public async Task Handle_InvalidQuery_ThrowsValidationException()
    {
        // Arrange
        var query = new GetAllProductQuery(PageNumber: 0, PageSize: 10, Order: "");

        // Act
        var act = () => _handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage("*Page must be greater than 0*");
    }

    [Fact(DisplayName = "Given valid query When getting products Then maps entity to result")]
    public async Task Handle_ValidQuery_MapsProductToResult()
    {
        // Arrange
        var query = new GetAllProductQuery(1, 10, "");

        var products = new List<Product>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Product Test",
                Description = "Description",
                Category = "Category",
                Price = 50.0m,
                Image = "image.jpg",
                Rating = new Rating(4.0m, 20)
            }
        };

        _repository.GetAllAsync(query.PageNumber, query.PageSize, query.Order, Arg.Any<CancellationToken>())
            .Returns((products, 1));

        _mapper.Map<IEnumerable<GetAllProductItemResult>>(products)
            .Returns(new List<GetAllProductItemResult>
            {
                new(products[0].Id, products[0].Title, products[0].Description, products[0].Category, products[0].Price, products[0].Image, new GetAllProductRatingResult(4.0m, 20))
            });

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _mapper.Received(1).Map<IEnumerable<GetAllProductItemResult>>(products);
    }
}
