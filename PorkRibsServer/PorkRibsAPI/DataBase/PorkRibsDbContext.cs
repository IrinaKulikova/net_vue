using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PorkRibs.Models;
using PorkRibsAPI.Models;

namespace PorkRibs.DataBase
{
    public class PorkRibsDbContext : IdentityDbContext<ApplicationUser>
    {
        public PorkRibsDbContext(DbContextOptions<PorkRibsDbContext> options)
            : base(options) { }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
