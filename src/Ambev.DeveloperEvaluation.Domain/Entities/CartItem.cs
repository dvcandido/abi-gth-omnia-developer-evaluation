using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class CartItem : BaseEntity
{
    public CartItem(Guid productId, string productTitle, int quantity)
    {
        ProductId = productId;
        ProductTitle = productTitle;
        Quantity = quantity;
    }

    protected CartItem() { }

    public Guid ProductId { get; private set; }
    public string ProductTitle { get; private set; } = string.Empty;
    public int Quantity { get; private set; }

    public Guid CartId { get; private set; }
    public Cart Cart { get; private set; } = null!;

    public void IncreaseQuantity(int quantity)
    {
        Quantity += quantity;
    }

    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }
}

