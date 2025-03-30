using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class ProductValidatorTests
    {
        private readonly ProductValidator _validator;

        public ProductValidatorTests()
        {
            _validator = new ProductValidator();
        }

        [Fact(DisplayName = "Valid product should pass all validation rules")]
        public void Given_ValidProduct_When_Validated_Then_ShouldNotHaveErrors()
        {
            var product = ProductTestData.GenerateValidProduct();
            var result = _validator.TestValidate(product);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory(DisplayName = "Invalid title formats should fail validation")]
        [MemberData(nameof(InvalidTitles))]
        public void Given_InvalidTitle_When_Validated_Then_ShouldHaveError(string title)
        {
            var product = ProductTestData.GenerateValidProduct();
            product.Title = title;
            var result = _validator.TestValidate(product);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact(DisplayName = "Description longer than maximum length should fail validation")]
        public void Given_LongDescription_When_Validated_Then_ShouldHaveError()
        {
            var product = ProductTestData.GenerateValidProduct();
            product.Description = new string('D', 501);
            var result = _validator.TestValidate(product);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact(DisplayName = "Category longer than maximum length should fail validation")]
        public void Given_LongCategory_When_Validated_Then_ShouldHaveError()
        {
            var product = ProductTestData.GenerateValidProduct();
            product.Category = new string('C', 51);
            var result = _validator.TestValidate(product);
            result.ShouldHaveValidationErrorFor(x => x.Category);
        }

        [Theory(DisplayName = "Invalid price should fail validation")]
        [InlineData(0)]
        [InlineData(-1)]
        public void Given_InvalidPrice_When_Validated_Then_ShouldHaveError(decimal price)
        {
            var product = ProductTestData.GenerateValidProduct();
            product.Price = price;
            var result = _validator.TestValidate(product);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Theory(DisplayName = "Invalid image URL should fail validation")]
        [InlineData("")] // Empty
        [InlineData("invalid-url")] // Invalid URL format
        public void Given_InvalidImage_When_Validated_Then_ShouldHaveError(string imageUrl)
        {
            var product = ProductTestData.GenerateValidProduct();
            product.Image = imageUrl;
            var result = _validator.TestValidate(product);
            result.ShouldHaveValidationErrorFor(x => x.Image);
        }

        [Fact(DisplayName = "Null rating should fail validation")]
        public void Given_NullRating_When_Validated_Then_ShouldHaveError()
        {
            var product = ProductTestData.GenerateValidProduct();
            product.Rating = null;
            var result = _validator.TestValidate(product);
            result.ShouldHaveValidationErrorFor(x => x.Rating);
        }

        public static IEnumerable<object[]> InvalidTitles()
        {
            yield return new object[] { "" }; // Empty
            yield return new object[] { "AB" }; // Less than 3 characters
            yield return new object[] { new string('A', 101) }; // More than 100 characters
        }
    }
}
