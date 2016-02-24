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
    public partial class tblTituloEducacionSuperior
    {
        [Display(Name= "Título Educación Superior Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Título Educación Superior Id es obligatorio")]
        public long tblTituloEducacionSuperior_ID { get; set; }
		
        [Display(Name= "Título Educación Superior Nombre")]
        [StringLength(255, ErrorMessage = "Máximo 255 caracteres")]

        public string titEduSup_nombre { get; set; }
		
        [Display(Name= "Título Educación Superior Año Graduación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> titEduSup_anoGraduacion { get; set; }
		
        [Display(Name= "Hoja Vida Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Título Educación Superior Año Graduación es obligatorio")]
        public long tblHojaVida_ID { get; set; }
		
        [Display(Name= "Nivel Académico Educación Superior Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nivel Académico Educación Superior Id es obligatorio")]
        public long tblNivelAcademicoEducacionSuperior_ID { get; set; }
		
    
        public virtual tblHojaVida tblHojaVida { get; set; }
        public virtual tblNivelAcademicoEducacionSuperior tblNivelAcademicoEducacionSuperior { get; set; }
    }
}
