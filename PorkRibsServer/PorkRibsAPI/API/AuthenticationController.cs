using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PorkRibsAPI.Factories.Interface;
using PorkRibsAPI.ViewModels;
using PorkRibsData.Models;
using PorkRibsRepositories.Interfaces;
using System.Threading.Tasks;

namespace PorkRibsAPI.API
{
    [Route("api/v1/[controller]/")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJWTTokenFactory _tokenFactory;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public AuthenticationController(UserManager<ApplicationUser> userManager,
                                        IJWTTokenFactory tokenFactory,
                                        IRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _tokenFactory = tokenFactory;
            _refreshTokenRepository = refreshTokenRepository;
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

            var token = await _tokenFactory.Create(user, roles);

            return Created("JWT", token);
        }


        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenDTO tokenDTO)
        {
            var refreshToken = _refreshTokenRepository
                    .FindByGUID(tokenDTO.RefreshToken, tokenDTO.UserName);

            if (refreshToken == null)
            {
                return BadRequest("Refresh token not found");
            }

            var user = await _userManager.FindByNameAsync(tokenDTO.UserName);
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            var token = await _tokenFactory.Create(user, roles);

            refreshToken.Revoked = true;
            await _refreshTokenRepository.Update(refreshToken);

            return Created("JWT", token);
        }
    }
}