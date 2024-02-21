using Dapper;
using Inventory.Products.DataAccess.Helpers.IHelpers;
using Inventory.Products.DataAccess.Models.Entites;
using Inventory.Products.DataAccess.Repositories.IRepositories;

namespace Inventory.Products.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDapperHelper _dapperHelper;
        public UsersRepository(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<int> CreateUser(UserEntity userEntity)
        {
            try
            {
                DynamicParameters p = new();
                p.Add("PuserName", userEntity.Name);
                p.Add("PuserEmail", userEntity.Email);
                p.Add("PuserPassword", userEntity.Password);
                return await _dapperHelper.ExecuteAsync("SpInsertUser", p);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
