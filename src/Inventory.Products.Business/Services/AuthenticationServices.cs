using Inventory.Products.Business.Services.IServices;
using Inventory.Products.DataAccess.Models.Configurations;
using Inventory.Products.DataAccess.Models.Entites;
using Inventory.Products.DataAccess.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inventory.Products.Business.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ILogger<AuthenticationServices> _logger;
        private readonly LogicConfiguration _logicConfiguration;
        public AuthenticationServices(ILogger<AuthenticationServices> logger, IOptions<LogicConfiguration> options, IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
            _logger = logger;
            _logicConfiguration = options.Value;
        }

        public async Task<AuthenticationEntity> AuthenticateUser(string email, string password)
        {
            try
            {
                var existUser = await _authenticationRepository.ValidateUser(email, password);
                if (existUser)
                {
                    var secretKey = _logicConfiguration.SecretKey;
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, email+":"+password),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var token = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return new()
                    {
                        JWT = tokenString,
                        ExpiredIn = 10
                    };
                }
                else
                {
                    throw new UnauthorizedAccessException("User or password is invalid");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: in AuthenticationServices AuthenticateUser {ex.Message}");
                throw;
            }
        }
    }
}
