using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Auth;
using Services.Interfaceses;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]LoginModel model)
        {
            var user = new UserDTO { Email = model.Email, Password = model.Password };
            await service.CreateUserAsync(user);

            return Ok();
        }
    }
}
