using Inventory.Products.DataAccess.Models.Entites;

namespace Inventory.Products.Business.Services.IServices
{
    public interface IAuthenticationServices
    {
        Task<AuthenticationEntity> AuthenticateUser(string email, string password);
    }
}
