using Dapper;
using Inventory.Products.Business.Services.IServices;
using Inventory.Products.DataAccess.Models.Dtos;
using Inventory.Products.DataAccess.Repositories.IRepositories;

namespace Inventory.Products.Business.Services
{
    public class CatalogsServices : ICatalogsServices
    {
        private readonly ICatalogsRepository _catalogRepository;
        public CatalogsServices(ICatalogsRepository catalogsRepository)
        {
            _catalogRepository = catalogsRepository;    
        }

        public async Task<List<ElaborationTypesDto>> GetElaborationTypes()
        {
            try
            {
                return await _catalogRepository.GetElaborationTypes();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        

        public async Task<List<ProductsStatusDto>> GetProductsStatus()
        {
            try
            {
                return await _catalogRepository.GetProductsStatus();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
