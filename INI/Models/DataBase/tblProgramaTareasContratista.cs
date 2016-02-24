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
    public partial class tblProgramaTareasContratista
    {
        [Display(Name="Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Id es obligatorio")]

        [Range(0,int.MaxValue)]
		public int Id { get; set; }
		
        [Display(Name= "Contratista Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Contratista Id es obligatorio")]
        [StringLength(128, ErrorMessage = "Máximo 128 caracteres")]

        public string Id_Contratista { get; set; }
		
        [Display(Name="Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre es obligatorio")]
        [RegularExpression(@"^[a-zA-Z0-9 ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Nombre")]
        [StringLength(128, ErrorMessage = "Máximo 128 caracteres")]


        public string Nombre { get; set; }
		
        [Display(Name="Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La Descripción es obligatoria")]

        public string Descripcion { get; set; }
		
        [Display(Name="Alternativa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La Alternativa es obligatoria")]

        [Range(0,int.MaxValue)]
		public int Alternativa { get; set; }
		
        [Display(Name="Responsabilidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La Responsabilidad es obligatorio")]

        [Range(0,int.MaxValue)]
		public int Responsabilidad { get; set; }
		
        [Display(Name= "Fecha de Inicio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La Fecha de Inicio es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Fecha_Ini { get; set; }
		
        [Display(Name= "Fecha final")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La Fecha final es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public System.DateTime Fecha_Fin { get; set; }
		
        [Display(Name="Estado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Estado es obligatorio")]

        [Range(0,int.MaxValue)]
		public int Estado { get; set; }
		
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual tblAlternativas tblAlternativas { get; set; }
        public virtual tblResponsabContratista tblResponsabContratista { get; set; }
    }
}