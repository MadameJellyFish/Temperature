using Api.Temeprature.Business.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Temperature.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthService _authService;

        public AuthController(UserManager<IdentityUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Votre logique d'authentification ici
            var user = await _userManager.FindByNameAsync(username);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var (accessToken, refreshToken) = await _authService.GenerateTokensAsync(username);

                return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
            }

            return Unauthorized();
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            try
            {
                var (accessToken, newRefreshToken) = await _authService.RefreshAsync(refreshToken).ConfigureAwait(false);

                return Ok(new { AccessToken = accessToken, RefreshToken = newRefreshToken });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}
