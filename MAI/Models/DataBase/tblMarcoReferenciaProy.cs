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
    
    public partial class tblMarcoReferenciaProy
    {
        public tblMarcoReferenciaProy()
        {
            this.tblProyectosInvestigacion = new HashSet<tblProyectosInvestigacion>();
        }
    
        public long tblMarcoReferenciaProy_ID { get; set; }
        public string marRefProy_marcoTeoricoProy { get; set; }
        public string marRefProy_marcoAntecedentesProy { get; set; }
        public string marRefProy_marcoConceptualProy { get; set; }
    
        public virtual ICollection<tblProyectosInvestigacion> tblProyectosInvestigacion { get; set; }
    }
}