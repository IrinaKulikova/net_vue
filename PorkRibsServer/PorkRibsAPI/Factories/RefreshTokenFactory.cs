using PorkRibsAPI.Factories.Interface;
using PorkRibsAPI.Models;
using System;

namespace PorkRibsAPI.Factories
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
