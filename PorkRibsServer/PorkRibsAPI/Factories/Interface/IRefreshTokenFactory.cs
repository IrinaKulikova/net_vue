using PorkRibsAPI.Models;

namespace PorkRibsAPI.Factories.Interface
{
    public interface IRefreshTokenFactory
    {
        RefreshToken Create(string token, string userName);
    }
}
