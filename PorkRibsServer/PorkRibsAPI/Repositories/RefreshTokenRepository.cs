using PorkRibs.DataBase;
using PorkRibsAPI.Models;
using PorkRibsAPI.Repositories.GenericRepository;

namespace PorkRibsAPI.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>
    {
        public RefreshTokenRepository(PorkRibsDbContext porkRibsDbContext):base(porkRibsDbContext)
        {}
    }
}