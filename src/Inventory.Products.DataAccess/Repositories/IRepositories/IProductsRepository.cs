using Inventory.Products.DataAccess.Models.Dtos;
using Inventory.Products.DataAccess.Models.Entites;

namespace Inventory.Products.DataAccess.Repositories.IRepositories
{
    public interface IProductsRepository
    {
        Task<SpResponseDto<List<ProductsListDto>>> GetProducts(ProductsFiltersEntity product);
        Task<int> CreateProduct(ProductEntity product);
        Task<int> UpdateProduct(ProductPutEntity product);
    }
}
