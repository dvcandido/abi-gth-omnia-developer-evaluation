using Ambev.DeveloperEvaluation.Application.Products.GetAllProductByCategory;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class GetAllProductByCategoryHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly GetAllProductByCategoryHandler _handler;

    public GetAllProductByCategoryHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetAllProductByCategoryHandler(_productRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid query When handling Then returns paginated result")]
    public async Task Handle_ValidQuery_ReturnsPaginatedResult()
    {
        // Arrange
        var query = new GetAllProductByCategoryQuery(1, 10, "title asc", "electronics");

        var products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Title = "Product A",
                Description = "Description A",
                Category = "electronics",
                Price = 100,
                Image = "img.jpg",
                Rating = new Rating { Rate = 4.5m, Count = 20 }
            }
        };

        var mappedResult = new List<GetAllProductByCategoryItemResult>
        {
            new GetAllProductByCategoryItemResult(
                products[0].Id,
                products[0].Title,
                products[0].Description,
                products[0].Category,
                products[0].Price,
                products[0].Image,
                new GetAllProductByCategoryRatingResult(products[0].Rating.Rate, products[0].Rating.Count))
        };

        _productRepository
            .GetAllByCategoryAsync(query.Category, query.PageNumber, query.PageSize, query.Order, Arg.Any<CancellationToken>())
            .Returns((products, 1));

        _mapper.Map<IEnumerable<GetAllProductByCategoryItemResult>>(products)
               .Returns(mappedResult);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.TotalItems.Should().Be(1);
        result.CurrentPage.Should().Be(1);
        result.TotalPages.Should().Be(1);
    }

    [Fact(DisplayName = "Given invalid query When handling Then throws validation exception")]
    public async Task Handle_InvalidQuery_ThrowsValidationException()
    {
        // Arrange
        var query = new GetAllProductByCategoryQuery(0, 0, "invalid-order", "");

        // Act
        var act = async () => await _handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
