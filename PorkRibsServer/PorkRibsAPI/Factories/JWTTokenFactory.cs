using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PorkRibs.Models;
using PorkRibsAPI.Factories.Interface;
using PorkRibsAPI.Models;
using PorkRibsAPI.Repositories.GenericRepository.Interfaces;
using PorkRibsAPI.Settings;
using PorkRibsAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PorkRibsAPI.Factories
{
    public class JWTTokenFactory : IJWTTokenFactory
    {
        private readonly JWTSettings _JWTSettings;
        private readonly IRefreshTokenFactory _refreshToken;
        private readonly IGenericRepository<RefreshToken> _refreshTokenRepository;

        public JWTTokenFactory(IOptions<JWTSettings> JWTSettings,
                               IRefreshTokenFactory refreshToken,
                               IGenericRepository<RefreshToken> refreshTokenRepository)
        {
            _JWTSettings = JWTSettings.Value;
            _refreshToken = refreshToken;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public TokenDTO Create(ApplicationUser user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_JWTSettings.Secret);

            var cliams = new List<Claim>();
            cliams.Add(new Claim(ClaimTypes.Name, user.Id));
            roles.ToList().ForEach(r => cliams.Add(new Claim(ClaimTypes.Role, r)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(cliams),
                Expires = DateTime.UtcNow.AddMinutes(_JWTSettings.MinutesLife),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                         SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            var refreshToken = _refreshToken.Create(token, user.UserName);
            _refreshTokenRepository.Create(refreshToken);

            var tokenDTO = new TokenDTO()
            {
                AccessToken = token,
                RefreshToken = refreshToken.Token,
                Roles = roles,
                Email = user.Email,
                Id = user.Id
            };

            return tokenDTO;
        }
    }
}
