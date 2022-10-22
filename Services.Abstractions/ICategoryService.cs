using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Abstractions
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<CategoryDto> GetByIdAsync(Guid CategoryId, CancellationToken cancellationToken = default);

        Task<CategoryDto> CreateAsync(CategoryForCreationDto CategoryForCreationDto, CancellationToken cancellationToken = default);

        Task UpdateAsync(Guid CategoryId, CategoryForUpdateDto CategoryForUpdateDto, CancellationToken cancellationToken = default);
        Task PatchDAsync(Guid CategoryId, JsonPatchDocument<CategoryForPatchDto> patchCategoryForUpdateDto, CancellationToken cancellationToken = default);
       
        Task DeleteAsync(Guid CategoryId, CancellationToken cancellationToken = default);
    }
}