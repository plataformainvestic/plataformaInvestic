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
    public partial class tblTutorZona
    {
        public tblTutorZona()
        {
            this.tblTutorZonaSedeEducativa = new HashSet<tblTutorZonaSedeEducativa>();
        }
    
        [Display(Name= "Tutor Zona Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Id es obligatorio")]

        public long tblTutorZona_ID { get; set; }
		
        [Display(Name= "Tutor Zona Nombre")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]

        public string tutZon_nombre { get; set; }
		
        [Display(Name= "Tutor Zona Apellido")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]

        public string tutZon_apellido { get; set; }
		
        [Display(Name= "Tutor Zona Correo")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]

        public string tutZon_correo { get; set; }
		
        [Display(Name= "Tutor Zona Teléfono")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]

		public string tutZon_telefono { get; set; }
		
        [Display(Name= "Institución Educativa Id")]
		[Range(0,int.MaxValue)]
		public Nullable<int> tblInstitucionEducativa_ID { get; set; }
		
        [Display(Name= "Está Activo")]
		public Nullable<bool> estaActivo { get; set; }
		
    
        public virtual tblInstitucion tblInstitucion { get; set; }
        public virtual ICollection<tblTutorZonaSedeEducativa> tblTutorZonaSedeEducativa { get; set; }
    }
}
