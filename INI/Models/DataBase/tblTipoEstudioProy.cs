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
    public partial class tblTipoEstudioProy
    {
        public tblTipoEstudioProy()
        {
            this.tblMetodoProy = new HashSet<tblMetodoProy>();
        }
    
        [Display(Name= "Tipo de estudio del proyecto Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Paradigma Epistemológico es obligatorio")]
        [Range(0,int.MaxValue)]
		public int tblTipoEstudioProy_ID { get; set; }
		
        [Display(Name= "Nombre Tipo de estudio del proyecto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre Tipo de estudio del proyecto es obligatorio")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Nombre Tipo de estudio del proyecto")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string tipEst_nombre { get; set; }
		
    
        public virtual ICollection<tblMetodoProy> tblMetodoProy { get; set; }
    }
}
