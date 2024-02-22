using Dapper;
using Inventory.Products.Business.Services.IServices;
using Inventory.Products.Business.Utils.IUilts;
using Inventory.Products.DataAccess.Models.Dtos;
using Inventory.Products.DataAccess.Models.Entites;
using Inventory.Products.DataAccess.Repositories.IRepositories;

namespace Inventory.Products.Business.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ISqsPublisher _sqsPublisher;
        public ProductServices(IProductsRepository productsRepository, ISqsPublisher sqsPublisher)
        {
            _productsRepository = productsRepository;
            _sqsPublisher = sqsPublisher;
        }

        public async Task<int> CreateProduct(ProductEntity productEntity)
        {
            try
            {
                var result = await _productsRepository.CreateProduct(productEntity);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateProduct(ProductPutEntity productEntity)
        {
            try
            {
                if (productEntity.Id == 0)
                {
                    throw new Exception("Id is required");
                }
                var result = await _productsRepository.UpdateProduct(productEntity);
                if (result != 1)
                {
                    throw new Exception("cError in update Product");
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


        public async Task CreateProductBulk(List<ProductEntity> productEntity)
        {
            try
            {
                foreach (var item in productEntity)
                {
                    await _sqsPublisher.PublishMessage(item);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
