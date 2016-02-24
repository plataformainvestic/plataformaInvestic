//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace INI.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    
    using System.ComponentModel.DataAnnotations;
    public partial class tblPresupuestoProy
    {
        public tblPresupuestoProy()
        {
            this.tblProyectosInvestigacion = new HashSet<tblProyectosInvestigacion>();
            this.tblRubroPresupuesto = new HashSet<tblRubroPresupuesto>();
        }
    
        [Display(Name= "Presupuesto Proyecto Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Id es obligatorio")]

        public long tblPresupuestoProy_ID { get; set; }
		
        [Display(Name= "Presupuesto Financiación Investic")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Presupuesto Financiación Investic es obligatorio")]

        public long pre_financiacionInvestic { get; set; }
		
        [Display(Name= "Presupuesto Total Investic")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ElPresupuesto Total Investic es obligatorio")]

        public long pre_totatInvestic { get; set; }
		
        [Display(Name= "Presupuesto Total Otra Fuente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Presupuesto Total Otra Fuente es obligatorio")]

        public long pre_totalOtraFuente { get; set; }
		
        [Display(Name= "Presupuesto Total")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Presupuesto Total es obligatorio")]

        public long pre_total { get; set; }
		
    
        public virtual ICollection<tblProyectosInvestigacion> tblProyectosInvestigacion { get; set; }
        public virtual ICollection<tblRubroPresupuesto> tblRubroPresupuesto { get; set; }
    }
}
