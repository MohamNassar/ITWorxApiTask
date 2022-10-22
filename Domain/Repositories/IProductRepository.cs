using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllByCategoryIdAsync(Guid CategoryId, CancellationToken cancellationToken = default);

        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Product> GetByIdAsync(Guid ProductId, CancellationToken cancellationToken = default);

        void Insert(Product Product);

        void Remove(Product Product);
    }
}
