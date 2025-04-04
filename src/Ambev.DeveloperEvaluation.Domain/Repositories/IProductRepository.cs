﻿using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<(IEnumerable<Product> Products, int TotalCount)> GetAllAsync(int page = 1, int size = 10, string order = "", CancellationToken cancellationToken = default);
        Task<(IEnumerable<Product> Products, int TotalCount)> GetAllByCategoryAsync(string category, int page = 1, int size = 10, string order = "", CancellationToken cancellationToken = default);
        Task<IEnumerable<string>> GetCategoriesAsync(CancellationToken cancellationToken = default);
        Task<Product?> CreateAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
