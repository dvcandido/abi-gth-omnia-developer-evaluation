using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData
{
    public static class UpdateProductHandlerTestData
    {
        private static readonly Faker<UpdateProductCommand> updateProductHandlerFaker = new Faker<UpdateProductCommand>()
            .CustomInstantiator(f => new UpdateProductCommand(
                f.Random.Guid(),
                f.Commerce.ProductName(),
                f.Lorem.Paragraph(),
                f.Commerce.Categories(1).First(),
                decimal.Parse(f.Commerce.Price(10, 1000)),
                f.Image.PicsumUrl(),
                new UpdateRatingCommand(
                    f.Random.Decimal(0, 5),
                    f.Random.Number(1, 1000)
                )
            ));

        public static UpdateProductCommand GenerateValidCommand()
        {
            return updateProductHandlerFaker.Generate();
        }
    }
}
