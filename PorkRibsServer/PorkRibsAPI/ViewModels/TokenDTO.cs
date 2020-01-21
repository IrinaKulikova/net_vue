using System.Collections.Generic;

namespace PorkRibsAPI.ViewModels
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
    }
}
