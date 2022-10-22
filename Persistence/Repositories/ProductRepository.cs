using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class ProductRepository : IProductRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public ProductRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Product>> GetAllByCategoryIdAsync(Guid CategoryId, CancellationToken cancellationToken = default) =>
            await _dbContext.Products.Where(x => x.CategoryId == CategoryId).ToListAsync(cancellationToken);

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default) =>
           await _dbContext.Products.ToListAsync(cancellationToken);

        public async Task<Product> GetByIdAsync(Guid ProductId, CancellationToken cancellationToken = default) =>
            await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == ProductId, cancellationToken);

        public void Insert(Product Product) => _dbContext.Products.Add(Product);

        public void Remove(Product Product) => _dbContext.Products.Remove(Product);
    }
}
