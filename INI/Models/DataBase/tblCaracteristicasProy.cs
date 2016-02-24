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
    public partial class tblCaracteristicasProy
    {
        public tblCaracteristicasProy()
        {
            this.tblProyectosInvestigacion = new HashSet<tblProyectosInvestigacion>();
        }
    
        [Display(Name= "Características Proyecto Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La Características Proyecto Id es obligatoria")]
        public long tblCaracteristicasProy_ID { get; set; }
		
        [Display(Name= "Características Proyecto Resultados Esperados")]
        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public string carProy_resultadosEsperadosProy { get; set; }
		
        [Display(Name= "Características proyecto caracterización")]
        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public string carProy_caracterizacionProy { get; set; }
		
    
        public virtual ICollection<tblProyectosInvestigacion> tblProyectosInvestigacion { get; set; }
    }
}
