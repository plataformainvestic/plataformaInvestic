using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace SEG.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; }
        public bool ConfirmedEmail { get; set; }

        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Celular { get; set; }
        public string Cargo { get; set; }
        public string Contrato { get; set; }
        public string Cdp { get; set; }
        public string Equipo { get; set; }
        public DateTime? Fecha_IniContrato { get; set; }
        public DateTime? Fecha_FinContrato { get; set; }
        public string LastName { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
        }
    }
}