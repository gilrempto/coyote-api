using Coyote.API.Application.Catalog;
using Coyote.API.Models.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace Coyote.API.Controllers.Catalog
{
    [ApiController]
    [Route("Catalog/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private readonly ProductAppService service;

        public ProductController(ILogger<ProductController> logger, ProductAppService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts()
        {
            return Ok(await service.ListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(Guid id)
        {
            var response = await service.FindAsync(id);
            return response != null ? Ok(response) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> PostProduct(ProductRequest request)
        {
            var response = await service.CreateAsync(request);
            return CreatedAtAction(nameof(GetProduct), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, ProductRequest request)
        {
            var response = await service.FindAsync(id);

            if (response == null)
                return NotFound();

            await service.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await service.FindAsync(id);

            if (product == null)
                return NotFound();

            await service.DeleteAsync(id);
            return NoContent();
        }
    }
}
