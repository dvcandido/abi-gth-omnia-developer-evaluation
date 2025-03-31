using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class CartItemValidatorTests
{
    private readonly CartItemValidator _validator = new();

    [Fact]
    public void Given_ValidItem_When_Validated_Then_ShouldBeValid()
    {
        // Arrange
        var item = new CartItem(Guid.NewGuid(), "Product A", 3, 10m);

        // Act
        var result = _validator.Validate(item);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Given_ZeroQuantity_When_Validated_Then_ShouldFail()
    {
        // Arrange
        var item = new CartItem(Guid.NewGuid(), "Product A", 0, 20m);

        // Act
        var result = _validator.Validate(item);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Quantity");
    }

    [Fact]
    public void Given_EmptyProductTitle_When_Validated_Then_ShouldFail()
    {
        // Arrange
        var item = new CartItem(Guid.NewGuid(), "", 1, 10m);

        // Act
        var result = _validator.Validate(item);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "ProductTitle");
    }
}
