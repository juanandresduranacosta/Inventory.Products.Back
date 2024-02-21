using Inventory.Products.Business.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Products.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private readonly ICatalogsServices _catalogsServices;
        public CatalogsController(ICatalogsServices catalogsServices)
        {
            _catalogsServices = catalogsServices;
        }
        
        [HttpGet("ElaborationTypes")]
        public async Task<IActionResult> GetElaborationTypes()
        {
            try
            {
                return Ok(await _catalogsServices.GetElaborationTypes());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet("ProductsStatus")]
        public async Task<IActionResult> GetProductsStatus()
        {
            try
            {
                return Ok(await _catalogsServices.GetProductsStatus());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
