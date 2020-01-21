using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PorkRibs.DataBase;
using PorkRibs.Models;
using PorkRibsAPI.DataBase.Intit;
using PorkRibsAPI.Enums;
using PorkRibsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PorkRibsAPI.DataBase.Init
{
    public class Initializer : IInitializer
    {
        private PorkRibsDbContext _context;
        private IConfiguration _configuration;
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        public Initializer(PorkRibsDbContext context,
                           IConfiguration configuration,
                           UserManager<ApplicationUser> userManager,
                           RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedDataAsync()
        {
            if (!_context.Roles.Any() && !_context.Users.Any() && !_context.UserRoles.Any())
            {
                var roleAdmin = new IdentityRole() { Name = nameof(UserRoleTypes.Admin) };
                var roleUser = new IdentityRole() { Name = nameof(UserRoleTypes.User) };

                await _roleManager.CreateAsync(roleAdmin);
                await _roleManager.CreateAsync(roleUser);
                await _context.SaveChangesAsync();

                var users = _configuration.GetSection("Users").Get<List<UserConfiguration>>();
                foreach (var userConfig in users)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = userConfig.UserName,
                        Email = userConfig.UserName,
                        EmailConfirmed = true,
                        LockoutEnabled = false
                    };
                    await _userManager.CreateAsync(user, userConfig.Password);
                    await _context.SaveChangesAsync();

                    await _userManager.AddToRoleAsync(user, userConfig.Role);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
