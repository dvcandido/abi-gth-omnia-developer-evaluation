using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class CartValidatorTests
{
    private readonly CartValidator _validator = new();

    [Fact]
    public void Given_ValidCart_When_Validated_Then_ShouldBeValid()
    {
        // Arrange
        var cart = new Cart(Guid.NewGuid(), DateTime.UtcNow);
        cart.SetUserInfo("John Doe");
        cart.AddItem(Guid.NewGuid(), "Sample Product", 2, 10m);

        // Act
        var result = _validator.Validate(cart);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Given_EmptyUserName_When_Validated_Then_ShouldHaveValidationError()
    {
        // Arrange
        var cart = new Cart(Guid.NewGuid(), DateTime.UtcNow);
        cart.SetUserInfo("");

        // Act
        var result = _validator.Validate(cart);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "UserName");
    }

    [Fact]
    public void Given_EmptyItems_When_Validated_Then_ShouldBeValid()
    {
        // Arrange
        var cart = new Cart(Guid.NewGuid(), DateTime.UtcNow);
        cart.SetUserInfo("Empty Cart");

        // Act
        var result = _validator.Validate(cart);

        //Assert
        result.IsValid.Should().BeTrue();
    }
}
