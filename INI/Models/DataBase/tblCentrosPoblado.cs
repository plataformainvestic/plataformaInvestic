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
    public partial class tblCentrosPoblado
    {
        [Display(Name= "Código Departamento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Código Departamento es obligatorio")]
        [StringLength(2, ErrorMessage = "Máximo 2 caracteres")]
        public string CodigoDepartamento { get; set; }
		
        [Display(Name= "Código Municipio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Código Municipio es obligatorio")]
        [StringLength(5, ErrorMessage = "Máximo 5 caracteres")]
        public string CodigoMunicipio { get; set; }
		
        [Display(Name= "Código Centro Poblado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Código Centro Poblado es obligatorio")]
        [StringLength(8, ErrorMessage = "Máximo 8 caracteres")]
        public string CodigoCentroPoblado { get; set; }
		
        [Display(Name="Nombre Departamento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre Departamento es obligatorio")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Nombre Departamento")]
        [StringLength(255, ErrorMessage = "Máximo 255 caracteres")]

        public string NombreDepartamento { get; set; }
		
        [Display(Name="Nombre Municipio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre Municipio es obligatorio")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Nombre Municipio")]
        [StringLength(255, ErrorMessage = "Máximo 255 caracteres")]

        public string NombreMunicipio { get; set; }
		
        [Display(Name="Nombre Centro Poblado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre Centro Poblado es obligatorio")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Nombre Centro Poblado")]
        [StringLength(255, ErrorMessage = "Máximo 255 caracteres")]

        public string NombreCentroPoblado { get; set; }
		
        [Display(Name="Tipo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Tipo es obligatorio")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Tipo")]
        [StringLength(5, ErrorMessage = "Máximo 5 caracteres")]

        public string Tipo { get; set; }
		
    }
}
