using PorkRibsAPI.TokenFactories.Interface;
using PorkRibsData.Models;
using System;

namespace PorkRibsAPI.TokenFactories
{
    public class RefreshTokenFactory : IRefreshTokenFactory
    {
        public RefreshToken Create(string token, string userName)
        {
            var refreshToken = new RefreshToken()
            {
                UserName = userName,
                Token = Guid.NewGuid().ToString(),
                Revoked = false
            };

            return refreshToken;
        }
    }
}
