using Inventory.Products.DataAccess.Models.Dtos;
using Inventory.Products.DataAccess.Models.Entites;

namespace Inventory.Products.Business.Services.IServices
{
    public interface IProductServices
    {
        Task<SpResponseDto<List<ProductsListDto>>> GetProducts(ProductsFiltersEntity product);
        Task CreateProduct(ProductEntity productEntity);
        Task UpdateProduct(ProductEntity productEntity);
    }
}
