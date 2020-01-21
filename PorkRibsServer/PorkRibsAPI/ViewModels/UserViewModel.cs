using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PorkRibsAPI.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
