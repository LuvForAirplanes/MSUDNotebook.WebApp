using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSUDTrack.DataModels.Models
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName, int authLevel) : base(roleName)
        {
            AuthLevel = authLevel;
        }

        public int AuthLevel { get; set; }
    }
}
