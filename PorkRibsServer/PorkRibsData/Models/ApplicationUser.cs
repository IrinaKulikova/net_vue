﻿using Microsoft.AspNetCore.Identity;

namespace PorkRibsData.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}
