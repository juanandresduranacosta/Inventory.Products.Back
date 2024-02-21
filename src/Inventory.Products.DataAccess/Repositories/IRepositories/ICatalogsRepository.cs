using Dapper;
using Inventory.Products.DataAccess.Models.Dtos;

namespace Inventory.Products.DataAccess.Repositories.IRepositories
{
    public interface ICatalogsRepository
    {
        Task<List<ElaborationTypesDto>> GetElaborationTypes();
        Task<ElaborationTypesDto?> GetElaborationTypesById(int id);
        Task<List<ProductsStatusDto>> GetProductsStatus();
        Task<ProductsStatusDto?> GetProductsStatus(int id);
    }
}
