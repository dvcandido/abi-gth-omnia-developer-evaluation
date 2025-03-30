using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class ProductTests
{
    [Fact(DisplayName = "Validation should pass for valid product data")]
    public void Given_ValidProductData_When_Validated_Then_ShouldReturnValid()
    {
        // Given
        var product = ProductTestData.GenerateValidProduct();

        // When
        var result = product.Validate();

        // Then
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact(DisplayName = "Validation should fail for invalid product data")]
    public void Given_InvalidProductData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Given
        var product = new Product
        {
            Title = "",
            Description = "",
            Category = "",
            Price = -10,
            Image = "invalid-url",
            Rating = null
        };

        // When
        var result = product.Validate();

        // Then
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
