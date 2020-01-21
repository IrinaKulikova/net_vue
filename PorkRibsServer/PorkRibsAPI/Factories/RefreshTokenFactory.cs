using PorkRibsAPI.Factories.Interface;
using PorkRibsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PorkRibsAPI.Factories
{
    public class RefreshTokenFactory : IRefreshTokenFactory
    {
        public RefreshToken Create(string token, string userName)
        {
            var refreshToken = new RefreshToken()
            {
                UserName = userName,
                Token = Guid.NewGuid().ToString()
            };

            return refreshToken;
        }
    }
}
