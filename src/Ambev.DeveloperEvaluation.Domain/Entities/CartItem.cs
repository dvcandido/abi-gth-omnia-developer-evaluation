using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class CartItem : BaseEntity
{
    public CartItem(Guid productId, string productTitle, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        ProductTitle = productTitle;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    protected CartItem() { }

    public Guid ProductId { get; private set; }
    public string ProductTitle { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public Guid CartId { get; private set; }
    public Cart Cart { get; private set; } = null!;
    public decimal Discount => CalculateDiscount();
    public decimal TotalPrice => (UnitPrice * Quantity) - Discount;

    private decimal CalculateDiscount()
    {
        if (Quantity >= 10 && Quantity <= 20)
            return UnitPrice * Quantity * 0.20m;
        if (Quantity >= 4)
            return UnitPrice * Quantity * 0.10m;
        return 0m;
    }

    public void IncreaseQuantity(int quantity)
    {
        Quantity += quantity;
    }

    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }
}

