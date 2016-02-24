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
    public partial class tblFechaCronograma
    {
        [Display(Name= "Fecha Cronograma Id")]
		[Required]
		public long tblFechaCronograma_ID { get; set; }
		
        [Display(Name= "Cronograma Proyecto Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Cronograma Proyecto Id es obligatorio")]
        public long tblCronogramaProy_ID { get; set; }
		
        [Display(Name= "Cronograma Actividad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Cronograma Actividad es obligatorio")]
        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public string cro_Actividad { get; set; }
		
        [Display(Name= "Cronograma Fecha Inicio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Cronograma Fecha Inicio es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime cro_FechaInicio { get; set; }
		
        [Display(Name= "Cronograma Fecha Fin")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Cronograma Fecha Fin es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime cro_FechaFin { get; set; }
		
        [Display(Name= "Cronograma Indicador")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Cronograma Indicador es obligatorio")]
        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public string cro_Indicador { get; set; }
		
    
        public virtual tblCronogramaProy tblCronogramaProy { get; set; }
    }
}
