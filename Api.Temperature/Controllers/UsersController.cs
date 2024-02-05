using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Temperature.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task Post()
        {
            IdentityUser user = new()
            {
                UserName = "Test",
                Email = "Test@gmail.com",
            };

            await _userManager.CreateAsync(user, "Chienbatu69.").ConfigureAwait(false);
        }


    }
}
