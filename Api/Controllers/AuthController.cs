using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Auth;
using Services.Interfaceses;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken(LoginModel loginModel)
        {
            string token = await authService.BuildTokenAsync(loginModel);
            if (token == null) return Unauthorized();
            return Ok(token);
        }
    }
}