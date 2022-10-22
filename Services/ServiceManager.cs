using System;
using AutoMapper;
using Domain.Repositories;
using Services.Abstractions;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICategoryService> _lazyCategoryService;
        private readonly Lazy<IProductService> _lazyProductService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _lazyCategoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, mapper));
            _lazyProductService = new Lazy<IProductService>(() => new ProductService(repositoryManager, mapper));
        }

        public ICategoryService CategoryService => _lazyCategoryService.Value;

        public IProductService ProductService => _lazyProductService.Value;
    }
}
