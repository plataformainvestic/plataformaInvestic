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
    public partial class tblEquiposTrabajo
    {
        [Display(Name="Equipo Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Equipo Id es obligatorio")]
        [Range(0,int.MaxValue)]
		public int Id_Equipo { get; set; }
		
        [Display(Name="Nombre Equipo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre Equipo es obligatorio")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Nombre Equipo")]
        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres")]

        public string Nombre_Equipo { get; set; }
		
        [Display(Name="Coordinador Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Coordinador Id es obligatorio")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Coordinador")]
        [StringLength(128, ErrorMessage = "Máximo 128 caracteres")]
        public string Id_Coordinador { get; set; }
		
        [Display(Name="Nombre Coordinador")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre Coordinador es obligatorio")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Nombre Coordinador")]
        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public string Nombre_Coordinador { get; set; }
		
    
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}