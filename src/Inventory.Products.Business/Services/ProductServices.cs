using Dapper;
using Inventory.Products.Business.Services.IServices;
using Inventory.Products.DataAccess.Models.Dtos;
using Inventory.Products.DataAccess.Models.Entites;
using Inventory.Products.DataAccess.Repositories.IRepositories;

namespace Inventory.Products.Business.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductsRepository _productsRepository;
        public ProductServices(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task CreateProduct(ProductEntity productEntity)
        {
            try
            {
                var result = await _productsRepository.CreateProduct(productEntity);
                if(result == 1)
                {
                    throw new Exception("Error in insert Product");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateProduct(ProductEntity productEntity)
        {
            try
            {
                var result = await _productsRepository.UpdateProduct(productEntity);
                if (result == 1)
                {
                    throw new Exception("Error in update Product");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<SpResponseDto<List<ProductsListDto>>> GetProducts(ProductsFiltersEntity product)
        {
            try
            {
                return await _productsRepository.GetProducts(product);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
