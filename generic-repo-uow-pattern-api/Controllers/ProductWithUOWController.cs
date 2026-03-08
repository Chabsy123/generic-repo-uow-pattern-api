using generic_repo_pattern_api.Entity;
using generic_repo_pattern_api.ViewModel;
using generic_repo_uow_pattern_api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace generic_repo_uow_pattern_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWithUOWController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductWithUOWController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _unitOfWork.GetRepository<Product>().GetAllAsync();
            return Ok(result);
        }

        [HttpGet("ProductPagging")]
        public async Task<IActionResult> GetProductPagging(int page = 1, int pageSize = 10, string searchTerm = null)
        {
            var productRepository = _unitOfWork.GetRepository<IProductRepository, Product>();
            var results = await productRepository.GetAllProductsWithPagging(page, pageSize, searchTerm);
            var metadata = new
            {
                results.TotalCount,
                results.PageSize,
                results.CurrentPage,
                results.TotalPages,
                results.HasNext,
                results.HasPrevious,
                results
            };
            return Ok(metadata);

        }

        [HttpGet("productbyname")]
        public async Task<IActionResult> GetByName(string productName)
        {
            var productRepository = _unitOfWork.GetRepository<IProductRepository, Product>();
            var result = await productRepository.GetProductsByName(productName);
            //var product = await _unitOfWork.ProductRepository.GetProductsByName(productName);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post(ProductRequest product)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var productEntity = new Product
                {
                    Price = product.Price,
                    ProductName = product.ProductName
                };

                await _unitOfWork.GetRepository<Product>().AddAsync(productEntity);
                await _unitOfWork.SaveChangesAsync();

                var orderEntity = new Order
                {
                    OrderDate = DateTime.UtcNow,
                    ProductId = productEntity.ProductId
                };
                await _unitOfWork.GetRepository<Order>().AddAsync(orderEntity);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();

                return StatusCode((int)HttpStatusCode.Created, new { Id = orderEntity.ProductId });
            }
            catch (System.Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
