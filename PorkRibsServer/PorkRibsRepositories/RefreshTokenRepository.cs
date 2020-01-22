using PorkRibsData.DataBase;
using PorkRibsData.Models;

namespace PorkRibsRepositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>
    {
        public RefreshTokenRepository(PorkRibsDbContext porkRibsDbContext):base(porkRibsDbContext)
        {}
    }
}