using INI.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INI.Models.Admin
{
    public class LogAcceso 
    {       

        
        public string Rol { get; set; }

        
        public string Usuario { get; set; }

        
        public string IP { get; set; }

        
        public string Latitud { get; set; }

        
        public string Longitud { get; set; }

        
        public string Altitud { get; set; }

        
        public string Navegacion { get; set; }

        
        public Nullable<System.DateTime> FechaInicioSesion { get; set; }

        
        public Nullable<System.DateTime> FechaCierreSesion { get; set; }
        public String Image { get; set; }
    }
}