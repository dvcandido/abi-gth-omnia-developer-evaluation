using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CartRepository : ICartRepository
{
    private readonly DefaultContext _context;

    public CartRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Cart> AddAsync(Cart cart, CancellationToken cancellationToken)
    {
        await _context.Carts.AddAsync(cart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return cart;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(id, cancellationToken);

        if (cart == null) return false;

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<(IEnumerable<Cart> Products, int TotalCount)> GetAllAsync(int page = 1, int size = 10, string order = "", CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsNoTracking();
        query = ApplyOrdering(order, query);

        var totalCount = await query.CountAsync(cancellationToken);
        var products = await query.Skip((page - 1) * size).Take(size).ToListAsync(cancellationToken);

        return (products, totalCount);
    }

    public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Cart?> UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        var existingCart = await GetByIdAsync(cart.Id, cancellationToken);

        if (existingCart == null)
            return null;

        existingCart.SetUserInfo(cart.UserName);

        foreach (var item in cart.Items)
        {
            existingCart.UpdateItem(item.ProductId, item.ProductTitle, item.Quantity);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return existingCart;
    }

    private static IQueryable<Cart> ApplyOrdering(string order, IQueryable<Cart> query)
    {
        if (!string.IsNullOrEmpty(order))
        {
            var orders = order.Split(',');

            IOrderedQueryable<Product>? orderedQuery = null;

            foreach (var o in orders)
            {
                var parts = o.Trim().Split(' ');
                if (parts.Length == 0) continue;

                var property = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(parts[0]);
                var direction = parts.Length > 1 ? parts[1] : "asc";

                if (!IsValidProperty<Product>(property)) continue;

                if (orderedQuery == null)
                {
                    orderedQuery = direction == "asc"
                        ? query.OrderBy(e => EF.Property<object>(e, property))
                        : query.OrderByDescending(e => EF.Property<object>(e, property));
                }
                else
                {
                    orderedQuery = direction == "asc"
                        ? orderedQuery.ThenBy(e => EF.Property<object>(e, property))
                        : orderedQuery.ThenByDescending(e => EF.Property<object>(e, property));
                }
            }

            return orderedQuery ?? query;
        }

        return query;
    }

    private static bool IsValidProperty<T>(string propertyName) => typeof(T).GetProperty(propertyName) != null;
}
