using PorkRibsData.Models;

namespace PorkRibsRepositories.Interfaces
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        RefreshToken FindByGUID(string token, string userName);
    }
}
