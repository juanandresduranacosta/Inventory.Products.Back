using Inventory.Products.DataAccess.Models.Dtos;

namespace Inventory.Products.Business.Services.IServices
{
    public interface ICatalogsServices
    {
        Task<List<ElaborationTypesDto>> GetElaborationTypes();
        Task<List<ProductsStatusDto>> GetProductsStatus();
    }
}
