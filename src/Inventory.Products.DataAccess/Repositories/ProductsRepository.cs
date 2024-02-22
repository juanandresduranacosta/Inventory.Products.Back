using Dapper;
using Inventory.Products.DataAccess.Helpers.IHelpers;
using Inventory.Products.DataAccess.Models.Dtos;
using Inventory.Products.DataAccess.Models.Entites;
using Inventory.Products.DataAccess.Repositories.IRepositories;

namespace Inventory.Products.DataAccess.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IDapperHelper _dapperHelper;
        public ProductsRepository(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<int> CreateProduct(ProductEntity product)
        {
            try
            {
                DynamicParameters p = new();
                p.Add("PtypeElaboration", product.TypeElaboration);
                p.Add("Pname", product.Name);
                p.Add("Pstatus", product.Status);
                //p.Add("PcreationUser", product.CreationUser);
                return await _dapperHelper.QueryFirstOrDefaultAsync<int>("SpInsertProducts", p);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateProduct(ProductPutEntity product)
        {
            try
            {
                DynamicParameters p = new();
                p.Add("Pid", product.Id);
                p.Add("Pstatus", product.StatusId);
                //p.Add("PcreationUser", product.CreationUser);
                return await _dapperHelper.ExecuteAsync("SpUpdateProducts", p);
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
                DynamicParameters p = new();
                p.Add("Ppage", product.page);
                p.Add("PpageSize", product.page_size);
                p.Add("PtypeElaboration", product.typeElaboration != 0 ? product.typeElaboration : null);
                p.Add("Pname", product.name);
                p.Add("Pstatus", product.status != 0 ? product.status : null);
                p.Add("PstartDate", product.startDate);
                p.Add("PendDate", product.endDate);
                return await _dapperHelper.ExecuteSp<ProductsListDto>("SpGetProducts", p);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
