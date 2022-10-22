using System;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<ICategoryRepository> _lazyCategoryRepository;
        private readonly Lazy<IProductRepository> _lazyProductRepository;
        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

        public RepositoryManager(RepositoryDbContext dbContext)
        {
            _lazyCategoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(dbContext));
            _lazyProductRepository = new Lazy<IProductRepository>(() => new ProductRepository(dbContext));
            _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
        }

        public ICategoryRepository CategoryRepository => _lazyCategoryRepository.Value;

        public IProductRepository ProductRepository => _lazyProductRepository.Value;

        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    }
}
