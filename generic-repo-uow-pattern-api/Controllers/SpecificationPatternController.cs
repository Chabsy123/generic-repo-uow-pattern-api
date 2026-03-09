using generic_repo_pattern_api.Entity;
using generic_repo_uow_pattern_api.Repository;
using generic_repo_uow_pattern_api.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace generic_repo_uow_pattern_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecificationPatternController : ControllerBase
    {

       private readonly IUnitOfWork _unitOfWork;

        public SpecificationPatternController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetProductsbyNamespecification")]
        public async Task<IActionResult> GetProductsbyNamespecification(string name)
        {
            var productRepository = _unitOfWork.GetRepository<IProductRepository, Product>();
            var spec = new ProductByNameSpec(name);
            var results = await productRepository.FindAsync(spec);
            return Ok(results);
        }

        [HttpGet("GetProductsbyIdSpecification")]
        public async Task<IActionResult> GetProductsbyIdSpecification(int id)
        {
            var productRepository = _unitOfWork.GetRepository<IProductRepository, Product>();
            var spec = new ProductByIdspec(id);
            var results = await productRepository.FindAsync(spec);
            return Ok(results);
        }

        [HttpGet("GetProductsbyNameOrderByspec")]
        public async Task<IActionResult> GetProductsbyNameOrderByspec(string search)
        {
            var productRepository = _unitOfWork.GetRepository<IProductRepository, Product>();
            var spec = new ProductsByNameOrderBySpec(search);
            var results = await productRepository.GetAllAsync(spec);
            return Ok(results);
        }

        [HttpGet("GetProductsByNameOrderByDescSpec")]
        public async Task<IActionResult> GetProductsByNameOrderByDescSpec(string search)
        {
            var productRepository = _unitOfWork.GetRepository<IProductRepository, Product>();
            var spec = new ProductsByNameOrderByDescSpec(search);
            var results = await productRepository.GetAllAsync(spec);
            return Ok(results);
        }

        [HttpGet("GetProductsByNameOrderByPagingSpec")]
        public async Task<IActionResult> GetProductsByNameOrderByPagingSpec(string search, int pageNumber, int pageSize)
        {
            var productRepository = _unitOfWork.GetRepository<IProductRepository, Product>();
            var spec = new ProductsByNameOrderByPagingSpec(search, pageNumber, pageSize);
            var results = await productRepository.GetAllAsync(spec);
            return Ok(results);
        }
    }
}
