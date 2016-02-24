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
    public partial class tblForoProyectoInvestigacion
    {
        public tblForoProyectoInvestigacion()
        {
            this.tblForoProyectoInvestigacion1 = new HashSet<tblForoProyectoInvestigacion>();
        }
    
        [Display(Name="id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Id es obligatorio")]
        [Range(0,int.MaxValue)]
		public int id { get; set; }
		
        [Display(Name="Usuario Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Usuario Id es obligatorio")]
        [StringLength(128, ErrorMessage = "Máximo 128 caracteres")]
        public string idUser { get; set; }
		
        [Display(Name= "Grupo Investigación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Grupo Investigación es obligatorio")]
        [Range(0,int.MaxValue)]
		public int idGrupoInvestigacion { get; set; }
		
        [Display(Name="Titulo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Titulo es obligatorio")]
        [RegularExpression(@"^[a-zA-Z0-9 ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para titulo")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]

        public string Titulo { get; set; }
		
        [Display(Name="Mensaje")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Mensaje es obligatorio")]
        [RegularExpression(@"^[a-zA-Z0-9 ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Mensaje")]
        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public string Mensaje { get; set; }
		
        [Display(Name="Fecha")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Fecha es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Fecha { get; set; }
		
        [Display(Name="Respuestas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La Respuestas es obligatoria")]
        [Range(0,int.MaxValue)]
		public int Respuestas { get; set; }
		
        [Display(Name="Foro Id")]
		[Range(0,int.MaxValue)]
		public Nullable<int> idForo { get; set; }
		
        [Display(Name= "Fecha Última Respuesta")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaUltimaRespuesta { get; set; }
		
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual ICollection<tblForoProyectoInvestigacion> tblForoProyectoInvestigacion1 { get; set; }
        public virtual tblForoProyectoInvestigacion tblForoProyectoInvestigacion2 { get; set; }
        public virtual tblGrupoInvestigacion tblGrupoInvestigacion { get; set; }
    }
}
