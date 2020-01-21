using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PorkRibs.Models;
using PorkRibsAPI.Factories.Interface;
using PorkRibsAPI.ViewModels;
using System.Threading.Tasks;

namespace PorkRibs.API
{
    [Route("api/v1/[controller]/")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJWTTokenFactory _tokenFactory;

        public AuthenticationController(UserManager<ApplicationUser> userManager,
                                        IJWTTokenFactory tokenFactory)
        {
            _userManager = userManager;
            _tokenFactory = tokenFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticateUser userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.UserName);

            if (user == null)
            {
                return BadRequest();
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return BadRequest();
            }

            var result = await _userManager.CheckPasswordAsync(user,
                             userModel.Password);

            if (!result)
            {
                return BadRequest();
            }

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            var token = _tokenFactory.Create(user, roles);

            return Created("JWT", token);
        }
    }
}