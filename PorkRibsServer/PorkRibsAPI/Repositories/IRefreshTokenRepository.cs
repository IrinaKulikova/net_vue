using PorkRibsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PorkRibsAPI.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task AddRefreshToken(RefreshToken token);
    }
}
