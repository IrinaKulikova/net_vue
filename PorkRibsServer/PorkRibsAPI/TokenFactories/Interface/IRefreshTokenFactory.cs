using PorkRibsData.Models;

namespace PorkRibsAPI.TokenFactories.Interface
{
    public interface IRefreshTokenFactory
    {
        RefreshToken Create(string token, string userName);
    }
}
