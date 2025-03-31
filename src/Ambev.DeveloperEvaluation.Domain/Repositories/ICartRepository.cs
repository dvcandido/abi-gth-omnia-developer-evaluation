using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICartRepository
{
    Task<Cart> AddAsync(Cart cart, CancellationToken cancellationToken);
    Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Cart?> UpdateAsync(Cart product, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(IEnumerable<Cart> Products, int TotalCount)> GetAllAsync(int page = 1, int size = 10, string order = "", CancellationToken cancellationToken = default);
}