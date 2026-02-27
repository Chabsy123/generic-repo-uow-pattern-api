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
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
