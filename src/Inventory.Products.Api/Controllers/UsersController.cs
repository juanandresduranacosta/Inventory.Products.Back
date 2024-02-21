using Inventory.Products.Business.Services.IServices;
using Inventory.Products.DataAccess.Models.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Products.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServices _usersService;
        public UsersController(IUsersServices usersServices)
        {
            _usersService = usersServices;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] UserEntity userEntity)
        {
            try
            {
                await _usersService.CreateUser(userEntity);
                return Ok(userEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
