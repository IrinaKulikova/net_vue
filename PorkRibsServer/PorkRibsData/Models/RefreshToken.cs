using System;

namespace PorkRibsData.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public bool Revoked { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
