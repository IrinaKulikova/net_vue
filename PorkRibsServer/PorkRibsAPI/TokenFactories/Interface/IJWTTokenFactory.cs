using PorkRibsAPI.ViewModels;
using PorkRibsData.Models;
using System.Collections.Generic;

namespace PorkRibsAPI.Factories.Interface
{
    public interface IJWTTokenFactory
    {
        TokenDTO Create(ApplicationUser user, IEnumerable<string> roles);
    }
}
