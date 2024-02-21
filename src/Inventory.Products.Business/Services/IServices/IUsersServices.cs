using Inventory.Products.DataAccess.Models.Entites;

namespace Inventory.Products.Business.Services.IServices
{
    public interface IUsersServices
    {
        Task CreateUser(UserEntity userEntity);
    }
}
