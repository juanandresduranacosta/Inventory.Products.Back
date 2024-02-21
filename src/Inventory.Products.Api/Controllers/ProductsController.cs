using Inventory.Products.Business.Services.IServices;
using Inventory.Products.DataAccess.Models.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace Inventory.Products.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;
        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductsFiltersEntity productsFilters)
        {
            //int page, int page_size, string? name, int status, DateTime? startDate, DateTime? endDate
            try
            {
                if (productsFilters.page == 0 || productsFilters.page_size == 0)
                {
                    throw new ArgumentException("Page or pagesize is required");
                }
                return Ok(await _productServices.GetProducts(productsFilters));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("byId")]
        public async Task<IActionResult> GetProducts([FromQuery] int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductEntity productEntity)
        {
            try
            {
                await _productServices.CreateProduct(productEntity);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductEntity productEntity)
        {
            try
            {
                await _productServices.UpdateProduct(productEntity);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> CreateProductBulk([FromBody] List<ProductEntity> productEntitys)
        {
            try
            {
                await _productServices.CreateProductBulk(productEntitys);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
