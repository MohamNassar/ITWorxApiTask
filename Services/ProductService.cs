using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Services.Abstractions;
using Services.Extensions;

namespace Services
{
    internal sealed class ProductService : IProductService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var Products = await _repositoryManager.ProductRepository.GetAllAsync(cancellationToken);

            var ProductsDto = _mapper.Map<IEnumerable<ProductDto>>(Products);

            return ProductsDto;
        }

        public async Task<IEnumerable<ProductDto>> GetAsync(Guid CategoryId, CancellationToken cancellationToken = default)
        {
            IEnumerable<Product> Products = null;
            if (CategoryId == default)
                Products = await _repositoryManager.ProductRepository.GetAllAsync(cancellationToken);
            else
                Products = await _repositoryManager.ProductRepository.GetAllByCategoryIdAsync(CategoryId, cancellationToken);

            var ProductsDto = _mapper.Map<IEnumerable<ProductDto>>(Products);

            return ProductsDto;
        }

        public async Task<ProductDto> GetByIdAsync(Guid ProductId, CancellationToken cancellationToken)
        {
            var Product = await _repositoryManager.ProductRepository.GetByIdAsync(ProductId, cancellationToken);

            if (Product is null)
            {
                throw new ProductNotFoundException(ProductId);
            }

            var ProductDto = _mapper.Map<ProductDto>(Product);

            return ProductDto;
        }

        public async Task<ProductDto> CreateAsync( ProductForCreationDto ProductForCreationDto, CancellationToken cancellationToken = default)
        {
            var Product = _mapper.Map<Product>(ProductForCreationDto);
            var Category = await _repositoryManager.CategoryRepository.GetByIdAsync(Product.CategoryId, cancellationToken);

            if (Category is null)
            {
                throw new CategoryNotFoundException(Product.CategoryId);
            }
            _repositoryManager.ProductRepository.Insert(Product);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductDto>(Product);
        }

        public async Task UpdateAsync(Guid ProductId, ProductForUpdateDto ProductForCreationDto, CancellationToken cancellationToken = default)
        {
            var Product = await _repositoryManager.ProductRepository.GetByIdAsync(ProductId, cancellationToken);

            if (Product is null)
            {
                throw new ProductNotFoundException(ProductId);
            }
            var Category = await _repositoryManager.CategoryRepository.GetByIdAsync(ProductForCreationDto.CategoryId, cancellationToken);

            if (Category is null)
            {
                throw new CategoryNotFoundException(Product.CategoryId);
            }
            Product.Name = ProductForCreationDto.Name;
            Product.Price = ProductForCreationDto.Price;
            Product.ImgURL = ProductForCreationDto.ImgURL;
            Product.Quantity = ProductForCreationDto.Quantity;
            Product.CategoryId = ProductForCreationDto.CategoryId;
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

        }
        public async Task PatchAsync(Guid productId, JsonPatchDocument<ProductForPatchDto> patchProductForUpdateDto, CancellationToken cancellationToken = default)
        {
            var product = await _repositoryManager.ProductRepository.GetByIdAsync(productId, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(productId);
            }
            var patchProduct = _mapper.Map<JsonPatchDocument<Product>>(patchProductForUpdateDto);
            patchProduct.ApplyTo(product);
            var productDto = _mapper.Map<ProductForPatchDto>(product);
            if (!productDto.IsValid(out List<Exception> exceptions))
            {
                throw new AggregateBadRequestException(exceptions);

            }
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync( Guid ProductId, CancellationToken cancellationToken = default)
        {

            var Product = await _repositoryManager.ProductRepository.GetByIdAsync(ProductId, cancellationToken);

            if (Product is null)
            {
                throw new ProductNotFoundException(ProductId);
            }
            _repositoryManager.ProductRepository.Remove(Product);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
