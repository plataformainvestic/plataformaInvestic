using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace INI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string SureName { get; set; }

        
        public string PersonalID { get; set; }

        public int Genre { get; set; }

        public string Address { get; set; }

        public DateTime BirthDay { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}