using System.Threading.Tasks;
using PorkRibs.DataBase;
using PorkRibsAPI.Models;

namespace PorkRibsAPI.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private PorkRibsDbContext _porkRibsDbContext;
        public RefreshTokenRepository(PorkRibsDbContext porkRibsDbContext)
        {
            _porkRibsDbContext = porkRibsDbContext;
        }
        public async Task AddRefreshToken(RefreshToken token)
        {
            await _porkRibsDbContext.RefreshTokens.AddAsync(token);
            await _porkRibsDbContext.SaveChangesAsync();
        }
    }
}
