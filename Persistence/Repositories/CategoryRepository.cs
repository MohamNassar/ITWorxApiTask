using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class CategoryRepository : ICategoryRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public CategoryRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _dbContext.Categories.Include(x => x.Products).ToListAsync(cancellationToken);

        public async Task<Category> GetByIdAsync(Guid CategoryId, CancellationToken cancellationToken = default) =>
            await _dbContext.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == CategoryId, cancellationToken);

        public void Insert(Category Category) => _dbContext.Categories.Add(Category);

        public void Remove(Category Category) => _dbContext.Categories.Remove(Category);
    }
}
