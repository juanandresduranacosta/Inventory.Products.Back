using Dapper;
using Inventory.Products.DataAccess.Helpers.IHelpers;
using Inventory.Products.DataAccess.Repositories.IRepositories;
using Microsoft.Extensions.Logging;

namespace Inventory.Products.DataAccess.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IDapperHelper _dapperHelper;
        private readonly ILogger<AuthenticationRepository> _logger;
        public AuthenticationRepository(IDapperHelper dapperHelper, ILogger<AuthenticationRepository> logger)
        {
            _dapperHelper = dapperHelper;
            _logger = logger;
        }

        public async Task<bool> ValidateUser(string email, string password)
        {
            try
            {
                DynamicParameters p = new();
                p.Add("Pemail", email); 
                p.Add("Ppassword", password);
                return await _dapperHelper.QueryFirstOrDefaultAsync<bool>("SpVerifyUser", p);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: in AuthenticationRepository ValidateUser {ex.Message}");
                throw;
            }
        }
    }
}
