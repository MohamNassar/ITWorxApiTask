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
    internal sealed class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public CategoryService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _repositoryManager.CategoryRepository.GetAllAsync(cancellationToken);

            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return categoriesDto;
        }

        public async Task<CategoryDto> GetByIdAsync(Guid CategoryId, CancellationToken cancellationToken = default)
        {
            var Category = await _repositoryManager.CategoryRepository.GetByIdAsync(CategoryId, cancellationToken);

            if (Category is null)
            {
                throw new CategoryNotFoundException(CategoryId);
            }

            var CategoryDto = _mapper.Map<CategoryDto>(Category);

            return CategoryDto;
        }

        public async Task<CategoryDto> CreateAsync(CategoryForCreationDto CategoryForCreationDto, CancellationToken cancellationToken = default)
        {
            var Category = _mapper.Map<Category>(CategoryForCreationDto);

            _repositoryManager.CategoryRepository.Insert(Category);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CategoryDto>(Category);
        }

        public async Task UpdateAsync(Guid CategoryId, CategoryForUpdateDto CategoryForUpdateDto, CancellationToken cancellationToken = default)
        {
            var Category = await _repositoryManager.CategoryRepository.GetByIdAsync(CategoryId, cancellationToken);

            if (Category is null)
            {
                throw new CategoryNotFoundException(CategoryId);
            }

            Category.Name = CategoryForUpdateDto.Name;
         

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid CategoryId, CancellationToken cancellationToken = default)
        {
            var Category = await _repositoryManager.CategoryRepository.GetByIdAsync(CategoryId, cancellationToken);

            if (Category is null)
            {
                throw new CategoryNotFoundException(CategoryId);
            }

            _repositoryManager.CategoryRepository.Remove(Category);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task PatchDAsync(Guid CategoryId, JsonPatchDocument<CategoryForPatchDto> patchCategoryForUpdateDto, CancellationToken cancellationToken = default)
        {
            var Category = await _repositoryManager.CategoryRepository.GetByIdAsync(CategoryId, cancellationToken);

            if (Category is null)
            {
                throw new CategoryNotFoundException(CategoryId);
            }
            var patchCategory = _mapper.Map<JsonPatchDocument<Category>>(patchCategoryForUpdateDto);
            patchCategory.ApplyTo(Category);
            var categoryDto = _mapper.Map<CategoryForPatchDto>(Category);
            if (!categoryDto.IsValid(out List<Exception> exceptions))
            {
                throw new AggregateBadRequestException(exceptions);

            }

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
        
    }
}