using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INI.Models.DataBase
{
    public class ReflexionDeLaOnda
    {
        public int id { get; set; }
        public String Introduccion { get; set; }
        public String ConformacionGrupo { get; set; }
        public String Objetivo { get; set; }
        public String ActividadesRealizadas { get; set; }
        public String ConceptosPrincipales { get; set; }

        public String EspaciosParticipacion { get; set; }
        public String Conclusiones { get; set; }        

    }
}