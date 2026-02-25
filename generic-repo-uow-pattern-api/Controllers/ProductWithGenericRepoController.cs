using generic_repo_pattern_api.Entity;
using generic_repo_pattern_api.Repository;
using generic_repo_pattern_api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace generic_repo_pattern_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWithGenericRepoController : ControllerBase
    {
        private readonly IRepository<Product> productRepository;

        public ProductWithGenericRepoController(IRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest product)
        {
            var newProduct = new Product
            {
                ProductName = product.ProductName,
                Price = product.Price
            };
            await productRepository.AddAsync(newProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ProductId }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequest product)
        {
            var existingProduct = await productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.ProductName = product.ProductName;
            existingProduct.Price = product.Price;
            await productRepository.UpdateAsync(existingProduct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existingProduct = await productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            await productRepository.DeleteAsync(existingProduct);
            return NoContent();
        }
    }
}
