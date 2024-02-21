using Dapper;
using Inventory.Products.Business.Services.IServices;
using Inventory.Products.DataAccess.Models.Entites;
using Inventory.Products.DataAccess.Repositories.IRepositories;

namespace Inventory.Products.Business.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly IUsersRepository _usersRepository;
        public UsersServices(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task CreateUser(UserEntity userEntity)
        {
            try
            {
                var result = await _usersRepository.CreateUser(userEntity);
                if(result != 1)
                {
                    throw new Exception("Error in insert user");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
