using Dapper;
using Inventory.Products.DataAccess.Helpers.IHelpers;
using Inventory.Products.DataAccess.Models.Dtos;
using Inventory.Products.DataAccess.Repositories.IRepositories;

namespace Inventory.Products.DataAccess.Repositories
{
    public class CatalogsRepository : ICatalogsRepository
    {
        private readonly IDapperHelper _dapperHelper;
        public CatalogsRepository(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<List<ElaborationTypesDto>> GetElaborationTypes()
        {
            try
            {
                return await _dapperHelper.QueryAsync<ElaborationTypesDto>("SpGetElaborationType", null);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<ElaborationTypesDto?> GetElaborationTypesById(int id)
        {
            try 
            {
                DynamicParameters p = new();
                p.Add("@Pid", id);
                return await _dapperHelper.QueryFirstOrDefaultAsync<ElaborationTypesDto>("SpGetElaborationType", p);
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
                return await _dapperHelper.QueryAsync<ProductsStatusDto>("SpGetProductsStatus", null);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<ProductsStatusDto?> GetProductsStatus(int id)
        {
            try
            {
                DynamicParameters p = new();
                p.Add("@Pid", id);
                return await _dapperHelper.QueryFirstOrDefaultAsync<ProductsStatusDto>("SpGetProductsStatus", p);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
