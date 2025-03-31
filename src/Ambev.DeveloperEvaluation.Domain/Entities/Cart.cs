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

    public void AddItem(Guid productId, string productTitle, int quantity)
    {
        var existingItem = Items.FirstOrDefault(x => x.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(quantity);
        }
        else
        {
            Items.Add(new CartItem(productId, productTitle, quantity));
        }
    }

    public void RemoveItem(Guid productId)
    {
        var item = Items.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
            Items.Remove(item);
    }

    public void UpdateItem(Guid productId, string productTitle, int quantity)
    {
        var item = Items.FirstOrDefault(x => x.ProductId == productId);

        if (item is null)
            AddItem(productId, productTitle, quantity);
        else
            item.UpdateQuantity(quantity);
    }

    public void SetUserInfo(string userName)
    {
        UserName = userName;
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
