using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Abstractions
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<ProductDto>> GetAsync(Guid CategoryId, CancellationToken cancellationToken = default);

        Task<ProductDto> GetByIdAsync(Guid ProductId, CancellationToken cancellationToken);

        Task<ProductDto> CreateAsync(ProductForCreationDto ProductForCreationDto, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid ProductId, ProductForUpdateDto ProductForCreationDto, CancellationToken cancellationToken = default);
        Task PatchAsync(Guid ProductId, JsonPatchDocument<ProductForPatchDto> patchProductForUpdateDto, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid ProductId, CancellationToken cancellationToken = default);
    }
}