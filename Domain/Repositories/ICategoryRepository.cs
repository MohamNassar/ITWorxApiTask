using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Category> GetByIdAsync(Guid CategoryId, CancellationToken cancellationToken = default);

        void Insert(Category Category);

        void Remove(Category Category);
    }
}
