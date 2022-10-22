using System;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CategoriesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var Categories = await _serviceManager.CategoryService.GetAllAsync(cancellationToken);

            return Ok(Categories);
        }

        [HttpGet("{CategoryId:guid}")]
        public async Task<IActionResult> GetCategoryById(Guid CategoryId, CancellationToken cancellationToken)
        {
            var CategoryDto = await _serviceManager.CategoryService.GetByIdAsync(CategoryId, cancellationToken);

            return Ok(CategoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreationDto CategoryForCreationDto)
        {
            var CategoryDto = await _serviceManager.CategoryService.CreateAsync(CategoryForCreationDto);

            return CreatedAtAction(nameof(GetCategoryById), new { CategoryId = CategoryDto.Id }, CategoryDto);
        }

        [HttpPut("{CategoryId:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid CategoryId, [FromBody] CategoryForUpdateDto CategoryForUpdateDto, CancellationToken cancellationToken)
        {
            await _serviceManager.CategoryService.UpdateAsync(CategoryId, CategoryForUpdateDto, cancellationToken);

            return NoContent();
        }

        [HttpPatch("{CategoryId:guid}")]
        public async Task<IActionResult> PatchCategory(Guid CategoryId, [FromBody] JsonPatchDocument<CategoryForPatchDto> patchCategoryDto, CancellationToken cancellationToken)
        {

            await _serviceManager.CategoryService.PatchDAsync(CategoryId, patchCategoryDto, cancellationToken);

            return NoContent();
        }
        [HttpDelete("{CategoryId:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid CategoryId, CancellationToken cancellationToken)
        {
            await _serviceManager.CategoryService.DeleteAsync(CategoryId, cancellationToken);

            return NoContent();
        }
    }
}
