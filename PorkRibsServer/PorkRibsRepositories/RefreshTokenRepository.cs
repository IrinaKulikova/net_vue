using PorkRibsData.DataBase;
using PorkRibsData.Models;
using PorkRibsRepositories.Interfaces;
using System.Linq;

namespace PorkRibsRepositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(PorkRibsDbContext porkRibsDbContext):base(porkRibsDbContext)
        {}

        public RefreshToken FindByGUID(string refreshToken, string userName)
        {
            var token =  _dbSet.Where(t=>t.Token == refreshToken
                                && t.UserName == userName).FirstOrDefault();

            return token;
        }
    }
}