using Inventory.Products.DataAccess.Models.Entites;

namespace Inventory.Products.DataAccess.Repositories.IRepositories
{
    public interface IUsersRepository
    {
        Task<int> CreateUser(UserEntity userEntity);
    }
}
