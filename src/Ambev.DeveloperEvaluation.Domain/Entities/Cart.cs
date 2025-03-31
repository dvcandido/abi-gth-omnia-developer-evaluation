using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Cart : BaseEntity
{
    public Cart(Guid userId, DateTime date)
    {
        UserId = userId;
        Date = date;
    }

    protected Cart() { }

    public Guid UserId { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public DateTime Date { get; private set; }

    public List<CartItem> Items { get; private set; } = [];

    public decimal Total => Items.Sum(x => x.TotalPrice);

    public void AddItem(Guid productId, string productTitle, int quantity, decimal unitPrice)
    {
        var existingItem = Items.FirstOrDefault(x => x.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(quantity);
        }
        else
        {
            Items.Add(new CartItem(productId, productTitle, quantity, unitPrice));
        }
    }

    public void RemoveItem(Guid productId)
    {
        var item = Items.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
            Items.Remove(item);
    }

    public void UpdateItem(Guid productId, string productTitle, int quantity, decimal unitPrice)
    {
        var item = Items.FirstOrDefault(x => x.ProductId == productId);

        if (item is null)
            AddItem(productId, productTitle, quantity, unitPrice);
        else
            item.UpdateQuantity(quantity);
    }

    public void SetUserInfo(string userName)
    {
        UserName = userName;
    }

    public void MergeItems(List<CartItem> newItems)
    {
        foreach (var newItem in newItems)
        {
            UpdateItem(newItem.ProductId, newItem.ProductTitle, newItem.Quantity, newItem.UnitPrice);
        }

        var newItemsIds = new HashSet<Guid>(newItems.Select(i => i.ProductId));
        Items.RemoveAll(i => !newItemsIds.Contains(i.ProductId));
    }

    public ValidationResultDetail Validate()
    {
        var validator = new CartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
}
