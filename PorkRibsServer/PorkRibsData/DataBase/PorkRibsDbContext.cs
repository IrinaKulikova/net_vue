using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PorkRibsData.Models;

namespace PorkRibsData.DataBase
{
    public class PorkRibsDbContext : IdentityDbContext<ApplicationUser>
    {
        public PorkRibsDbContext(DbContextOptions<PorkRibsDbContext> options)
            : base(options) { }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
