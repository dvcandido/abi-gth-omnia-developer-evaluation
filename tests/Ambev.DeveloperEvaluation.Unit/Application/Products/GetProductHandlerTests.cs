using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class GetProductHandlerTests
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly GetProductHandler _handler;

        public GetProductHandlerTests()
        {
            _repository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetProductHandler(_repository, _mapper);
        }

        [Fact(DisplayName = "Given valid product id When getting product Then returns product result")]
        public async Task GivenValidProductId_WhenGettingProduct_ThenReturnsProductResult()
        {
            // Given
            var productId = Guid.NewGuid();
            var query = new GetProductQuery(productId);

            var product = new Product
            {
                Id = productId,
                Title = "Produto Exemplo",
                Description = "Descrição do produto",
                Category = "Categoria Exemplo",
                Price = 99.99m,
                Image = "imagem.jpg",
                Rating = new Rating(4.5m, 200)
            };

            var expectedResult = new GetProductResult(
                product.Id,
                product.Title,
                product.Description,
                product.Category,
                product.Price,
                product.Image,
                new GetRatingResult(product.Rating.Rate, product.Rating.Count)
            );

            _repository.GetByIdAsync(productId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(product));
            _mapper.Map<GetProductResult>(product).Returns(expectedResult);

            // When
            var result = await _handler.Handle(query, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
            await _repository.Received(1).GetByIdAsync(productId, Arg.Any<CancellationToken>());
            _mapper.Received(1).Map<GetProductResult>(product);
        }

        [Fact(DisplayName = "Given invalid product id When getting product Then throws validation exception")]
        public async Task GivenInvalidProductId_WhenGettingProduct_ThenThrowsValidationException()
        {
            // Given
            var query = new GetProductQuery(Guid.Empty); // Guid vazio dispara a validação

            // When
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Given valid product id When product not found Then throws key not found exception")]
        public async Task GivenValidProductId_WhenProductNotFound_ThenThrowsKeyNotFoundException()
        {
            // Given
            var productId = Guid.NewGuid();
            var query = new GetProductQuery(productId);

            _repository.GetByIdAsync(productId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<Product>(null));

            // When
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"User with ID {productId} not found");
        }
    }
}
