using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PorkRibsAPI.Factories.Interface;
using PorkRibsAPI.ViewModels;
using PorkRibsData.DataBase.Intit.Interfaces;
using PorkRibsData.Enums;
using PorkRibsData.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PorkRibsAPI.API
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IInitializer _initializer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJWTTokenFactory _tokenFactory;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(IInitializer initializer,
                               UserManager<ApplicationUser> userManager,
                               IJWTTokenFactory tokenFactory,
                               RoleManager<IdentityRole> roleManager)
        {
            _initializer = initializer;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenFactory = tokenFactory;
        }

        [HttpGet]
        public async Task SeedData()
        {
            await _initializer.SeedDataAsync();
        }

        [HttpGet]
        [Route("users")]
        [Authorize(Roles = nameof(UserRoleTypes.Admin))]
        public IEnumerable<UserViewModel> GetUsers()
        {
            var users = _userManager.Users.Select(u => new UserViewModel()
            {
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName
            });

            return users;
        }
    }
}