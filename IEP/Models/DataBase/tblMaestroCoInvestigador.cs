//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IEP.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblMaestroCoInvestigador
    {
        public int id { get; set; }
        public string idUsuario { get; set; }
        public Nullable<int> TiempoOndas { get; set; }
        public string Pregrado { get; set; }
        public string Postgrado { get; set; }
        public string Otro { get; set; }
        public int idAreaConocimiento { get; set; }
        public string ExperienciaAreaConocimiento { get; set; }
        public int idInstitucion { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual tblAreaConocimiento tblAreaConocimiento { get; set; }
        public virtual tblInstitucion tblInstitucion { get; set; }
    }
}
