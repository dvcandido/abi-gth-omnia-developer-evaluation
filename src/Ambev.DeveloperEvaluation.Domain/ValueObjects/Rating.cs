namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Rating
    {
        public Rating()
        {
        }

        public Rating(decimal rate, int count)
        {
            Rate = rate;
            Count = count;
        }

        public decimal Rate { get; set; }
        public int Count { get; set; }
    }
}
