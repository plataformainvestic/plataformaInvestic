using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INI.Models.DataBase;

namespace INI.Models.Database.Adds
{
    public class Tutores
    {
        public string nomregion { get; set; }
        public string municipio { get; set; }
        public string institucioneducativa { get; set; }
        public List <string> nombretutor { get; set; }
        public List<string> apellidotutor { get; set; }
        public List<string> correoelectronico { get; set; }
        public List<string> telefono { get; set; }

    }
    public class Asesores
    {
        public tblAsesorZona asesores { get; set; }

    }
    public class Noticias
    { 
    //aqui va la tabla de noticias
    }
}