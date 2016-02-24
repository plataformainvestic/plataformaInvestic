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
    
    public partial class tblInstitucion
    {
        public tblInstitucion()
        {
            this.tblGrupoInvestigacion = new HashSet<tblGrupoInvestigacion>();
            this.tblMaestroCoInvestigador = new HashSet<tblMaestroCoInvestigador>();
        }
    
        public int id { get; set; }
        public string CodigoDane { get; set; }
        public string ConsecutivoSede { get; set; }
        public string Nombre { get; set; }
        public string NombreSede { get; set; }
        public int idTipoInstitucion { get; set; }
        public string idMunicipio { get; set; }
        public Nullable<int> idZona { get; set; }
        public string Direccion { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string NombreDirector { get; set; }
        public string TelefonoDirector { get; set; }
        public string CorreoDirector { get; set; }
        public string SitioWeb { get; set; }
    
        public virtual ICollection<tblGrupoInvestigacion> tblGrupoInvestigacion { get; set; }
        public virtual tblMunicipios tblMunicipios { get; set; }
        public virtual tblTipoInstitucion tblTipoInstitucion { get; set; }
        public virtual tblZona tblZona { get; set; }
        public virtual ICollection<tblMaestroCoInvestigador> tblMaestroCoInvestigador { get; set; }
    }
}
