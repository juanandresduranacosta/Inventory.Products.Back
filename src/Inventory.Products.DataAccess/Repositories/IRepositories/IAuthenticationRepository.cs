namespace Inventory.Products.DataAccess.Repositories.IRepositories
{
    public interface IAuthenticationRepository
    {
        Task<bool> ValidateUser(string email, string password);
    }
}
