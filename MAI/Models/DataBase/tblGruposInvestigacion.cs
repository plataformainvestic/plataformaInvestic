//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MAI.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblGruposInvestigacion
    {
        public tblGruposInvestigacion()
        {
            this.tblIntegrantesGrupoInv = new HashSet<tblIntegrantesGrupoInv>();
            this.tblProyectosInvestigacion = new HashSet<tblProyectosInvestigacion>();
        }
    
        public long tblGruposInvestigacion_ID { get; set; }
        public string tblUsuarioPlataforma_ID { get; set; }
        public string gruInv_nombreGrupo { get; set; }
        public string gruInv_emblema { get; set; }
        public long tblEstado_ID { get; set; }
        public System.DateTime gruInv_fechaCreacion { get; set; }
        public int gruInv_proyectos { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual tblEstado tblEstado { get; set; }
        public virtual ICollection<tblIntegrantesGrupoInv> tblIntegrantesGrupoInv { get; set; }
        public virtual ICollection<tblProyectosInvestigacion> tblProyectosInvestigacion { get; set; }
    }
}
