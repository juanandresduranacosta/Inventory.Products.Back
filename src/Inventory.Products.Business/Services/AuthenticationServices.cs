using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
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
                AmazonCognitoIdentityProviderClient provider =
                        new ();
                CognitoUserPool userPool = new(_logicConfiguration.UserPool, _logicConfiguration.ClientId, provider);
                CognitoUser user = new (email, _logicConfiguration.ClientId, userPool, provider);
                InitiateSrpAuthRequest authRequest = new ()
                {
                    Password = password
                };
                AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
                if (authResponse != null)
                {
                    // Decodificar el JWT existente
                    var existingToken = new JwtSecurityToken(authResponse.AuthenticationResult.AccessToken);

                    // Crear una nueva firma para el JWT
                    var signingCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_logicConfiguration.SecretKey)),
                        SecurityAlgorithms.HmacSha256
                    );

                    // Crear un nuevo token con la nueva firma
                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateJwtSecurityToken(
                        existingToken.Issuer,
                        null,
                        null,
                        null,
                        null,
                        existingToken.IssuedAt,
                        signingCredentials
                    );

                    // Generar el JWT firmado
                    string signedJwt = handler.WriteToken(securityToken);




                    return new()
                    {
                        JWT = signedJwt,
                        ExpiredIn = authResponse.AuthenticationResult.ExpiresIn,
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
                throw new UnauthorizedAccessException("User or password is invalid");
            }
        }
    }
}
