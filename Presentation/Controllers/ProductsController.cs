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
    [Route("api/Products")]
    public class ProductsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ProductsController(IServiceManager serviceManager) => _serviceManager = serviceManager;
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] Guid CategoryId, CancellationToken cancellationToken)
        {
            var ProductsDto = await _serviceManager.ProductService.GetAsync(CategoryId, cancellationToken);

            return Ok(ProductsDto);
        }
       
        [HttpGet("{ProductId:guid}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid ProductId, CancellationToken cancellationToken)
        {
            var ProductDto = await _serviceManager.ProductService.GetByIdAsync(ProductId, cancellationToken);

            return Ok(ProductDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationDto ProductForCreationDto, CancellationToken cancellationToken)
        {
            var response = await _serviceManager.ProductService.CreateAsync(ProductForCreationDto, cancellationToken);

            return CreatedAtAction(nameof(GetProductById), new { ProductId = response.Id }, response);
        }

        [HttpPut("{ProductId:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid ProductId, [FromBody] ProductForUpdateDto ProductForCreationDto, CancellationToken cancellationToken)
        {
            await _serviceManager.ProductService.UpdateAsync(ProductId, ProductForCreationDto, cancellationToken);

            return NoContent();
        }
        [HttpPatch("{ProductId:guid}")]
        public async Task<IActionResult> PatchProduct(Guid ProductId, [FromBody] JsonPatchDocument<ProductForPatchDto> patchProductDto, CancellationToken cancellationToken)
        {
            await _serviceManager.ProductService.PatchAsync(ProductId, patchProductDto, cancellationToken);

            return NoContent();
        }
        [HttpDelete("{ProductId:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid ProductId, CancellationToken cancellationToken)
        {
            await _serviceManager.ProductService.DeleteAsync(ProductId, cancellationToken);

            return NoContent();
        }
    }
}