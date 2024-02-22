using Inventory.Products.DataAccess.Models.Dtos;
using Inventory.Products.DataAccess.Models.Entites;

namespace Inventory.Products.Business.Services.IServices
{
    public interface IProductServices
    {
        Task<SpResponseDto<List<ProductsListDto>>> GetProducts(ProductsFiltersEntity product);
        Task<int> CreateProduct(ProductEntity productEntity);
        Task UpdateProduct(ProductPutEntity productEntity);
        Task CreateProductBulk(List<ProductEntity> productEntity);
    }
}
