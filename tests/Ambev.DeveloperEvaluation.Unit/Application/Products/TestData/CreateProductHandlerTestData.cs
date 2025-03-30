using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData
{
    public static class CreateProductHandlerTestData
    {
        private static readonly Faker<CreateProductCommand> createProductHandlerFaker = new Faker<CreateProductCommand>()
        .CustomInstantiator(f => new CreateProductCommand(
            f.Commerce.ProductName(),
            f.Lorem.Paragraph(),
            f.Commerce.Categories(1).First(),
            decimal.Parse(f.Commerce.Price(10, 1000)),
            f.Image.PicsumUrl(),
            new CreateRatingCommand(
                f.Random.Decimal(0, 5),
                f.Random.Number(1, 1000)
            )
        ));

        public static CreateProductCommand GenerateValidCommand()
        {
            return createProductHandlerFaker.Generate();
        }
    }
}
