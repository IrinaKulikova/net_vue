using PorkRibsAPI.ViewModels;
using PorkRibsData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PorkRibsAPI.Factories.Interface
{
    public interface IJWTTokenFactory
    {
        Task<TokenDTO> Create(ApplicationUser user, IEnumerable<string> roles);
    }
}
