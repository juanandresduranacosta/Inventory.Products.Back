using Inventory.Products.Business.Services.IServices;
using Inventory.Products.DataAccess.Models.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Products.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServices _authenticationServices;
        public AuthenticationController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        [HttpPost("Signin")]
        public async Task<IActionResult> Signin([FromBody] SigninEntity signinEntity)
        {
            try
            {
                return Ok(await _authenticationServices.AuthenticateUser(signinEntity.email, signinEntity.password));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
