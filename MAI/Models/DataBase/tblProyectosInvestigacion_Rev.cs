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
    
    public partial class tblProyectosInvestigacion_Rev
    {
        public tblProyectosInvestigacion_Rev()
        {
            this.tblProyectosInvestigacion = new HashSet<tblProyectosInvestigacion>();
            this.tblProyectosInvestigacion1 = new HashSet<tblProyectosInvestigacion>();
            this.tblProyectosInvestigacion2 = new HashSet<tblProyectosInvestigacion>();
            this.tblProyectosInvestigacion3 = new HashSet<tblProyectosInvestigacion>();
        }
    
        public long tblProyectosInvestigacion_Rev_ID { get; set; }
        public long tblProyectosInvestigacion_ID { get; set; }
        public long tblEstado_ID { get; set; }
        public long tblPresentacionProyecto_Rev_ID { get; set; }
        public long tblProblemaInvestigacionProy_Rev_ID { get; set; }
        public long tblMarcoReferenciaProy_Rev_ID { get; set; }
        public long tblMetodoProy_Rev_ID { get; set; }
        public long tblCaracteristicasProy_Rev_ID { get; set; }
        public long tblCronogramaProy_Rev_ID { get; set; }
        public long tblPresupuestoProy_Rev_ID { get; set; }
        public long tblReferenciasProy_Rev_ID { get; set; }
        public string tblUsuarioPlataforma_Evaluador_ID { get; set; }
        public Nullable<System.DateTime> proyInvRev_fechaAsignacion { get; set; }
        public Nullable<System.DateTime> proyInvRev_fechaLimiteEvaluacion { get; set; }
        public Nullable<System.DateTime> proyInvRev_fechaUltimaEvaluacion { get; set; }
    
        public virtual tblCaracteristicasProy_Rev tblCaracteristicasProy_Rev { get; set; }
        public virtual tblCronogramaProy_Rev tblCronogramaProy_Rev { get; set; }
        public virtual tblEstado tblEstado { get; set; }
        public virtual tblMarcoReferenciaProy_Rev tblMarcoReferenciaProy_Rev { get; set; }
        public virtual tblMetodoProy_Rev tblMetodoProy_Rev { get; set; }
        public virtual tblPresentacionProyecto_Rev tblPresentacionProyecto_Rev { get; set; }
        public virtual tblPresupuestoProy_Rev tblPresupuestoProy_Rev { get; set; }
        public virtual tblProblemaInvestigacionProy_Rev tblProblemaInvestigacionProy_Rev { get; set; }
        public virtual ICollection<tblProyectosInvestigacion> tblProyectosInvestigacion { get; set; }
        public virtual ICollection<tblProyectosInvestigacion> tblProyectosInvestigacion1 { get; set; }
        public virtual ICollection<tblProyectosInvestigacion> tblProyectosInvestigacion2 { get; set; }
        public virtual ICollection<tblProyectosInvestigacion> tblProyectosInvestigacion3 { get; set; }
        public virtual tblReferenciasProy_Rev tblReferenciasProy_Rev { get; set; }
    }
}
