using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DefaultContext _context;

        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Product?> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await GetByIdAsync(id, cancellationToken);

            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }

        public async Task<IEnumerable<Product>> GetAllAsync(int page = 1, int size = 10, string order = "", CancellationToken cancellationToken = default)
        {
            var query = _context.Products.AsNoTracking();
            query = ApplyOrdering(order, query);

            return await query.Skip((page - 1) * size).Take(size).ToListAsync(cancellationToken);

        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category, int page = 1, int size = 10, string order = "", CancellationToken cancellationToken = default)
        {
            var query = _context.Products.AsNoTracking();
            query = ApplyOrdering(order, query);

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }

            return await query.Skip((page - 1) * size).Take(size).ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            var existingProduct = await GetByIdAsync(product.Id, cancellationToken);
            if (existingProduct == null)
            {
                return null;
            }

            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            
            existingProduct.Rating = product.Rating;

            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }
        private static IQueryable<Product> ApplyOrdering(string order, IQueryable<Product> query)
        {
            if (!string.IsNullOrEmpty(order))
            {
                var orders = order.Split(',');

                foreach (var o in orders)
                {
                    var parts = o.Trim().Split(' ');
                    if (parts.Length == 0) continue;

                    var property = parts[0];
                    var direction = parts.Length > 1 ? parts[1] : "asc";

                    if (!IsValidProperty<Product>(property)) continue;

                    if (direction == "asc")
                    {
                        query = query.OrderBy(e => EF.Property<object>(e, property));
                    }
                    else
                    {
                        query = query.OrderByDescending(e => EF.Property<object>(e, property));
                    }
                }
            }

            return query;
        }

        private static bool IsValidProperty<T>(string propertyName) => typeof(T).GetProperty(propertyName) != null;
    }
}
