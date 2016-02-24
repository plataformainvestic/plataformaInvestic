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
    public partial class tblHerramientasRecoleccionInformacion
    {
        public tblHerramientasRecoleccionInformacion()
        {
            this.tblRecoleccionInformacionProyectoInvestigacion = new HashSet<tblRecoleccionInformacionProyectoInvestigacion>();
        }
    
        [Display(Name="Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Id es obligatorio")]
        [Range(0,int.MaxValue)]
		public int id { get; set; }
		
        [Display(Name= "Herramienta Recolección")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La herramienta recolección es obligatoria")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para la herramienta recolección")]
        [StringLength(255, ErrorMessage = "Máximo 255 caracteres")]
        public string HerramientaRecoleccion { get; set; }
		
        [Display(Name= "Descripción")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para la descripción")]
        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public string Descripcion { get; set; }
		
    
        public virtual ICollection<tblRecoleccionInformacionProyectoInvestigacion> tblRecoleccionInformacionProyectoInvestigacion { get; set; }
    }
}