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
    public partial class tblInstitucion
    {
        public tblInstitucion()
        {
            this.tblGrupoInvestigacion = new HashSet<tblGrupoInvestigacion>();
            this.tblHojaVida = new HashSet<tblHojaVida>();
            this.tblMaestroCoInvestigador = new HashSet<tblMaestroCoInvestigador>();
            this.tblTutorZona = new HashSet<tblTutorZona>();
        }
    
        [Display(Name="Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Id es obligatorio")]
        [Range(0,int.MaxValue)]
		public int id { get; set; }
		
        [Display(Name= "Código Dane")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Código Dane es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string CodigoDane { get; set; }
		
        [Display(Name="Consecutivo Sede")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Consecutivo Sede es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string ConsecutivoSede { get; set; }
		
        [Display(Name="Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre es obligatorio")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Nombre")]
        [StringLength(150, ErrorMessage = "Máximo 150 caracteres")]

        public string Nombre { get; set; }
		
        [Display(Name="Nombre Sede")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Nombre Sede")]
        [StringLength(150, ErrorMessage = "Máximo 150 caracteres")]
        public string NombreSede { get; set; }
		
        [Display(Name= "Tipo Institución Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Tipo Institución Id es obligatorio")]
        public long idTipoInstitucion { get; set; }
		
        [Display(Name="Municipio Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Municipio Id es obligatorio")]
        [StringLength(5, ErrorMessage = "Máximo 5 caracteres")]
        public string idMunicipio { get; set; }
		
        [Display(Name="Zona Id")]
		[Range(0,int.MaxValue)]
		public Nullable<int> idZona { get; set; }
		
        [Display(Name= "Dirección")]
        [StringLength(250, ErrorMessage = "Máximo 250 caracteres")]
        public string Direccion { get; set; }
		
        [Display(Name= "Latitud")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Latitud { get; set; }
		
        [Display(Name= "Longitud")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Longitud { get; set; }
		
        [Display(Name= "Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El teléfono es obligatorio")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [RegularExpression(@"^[0-9 ]+$", ErrorMessage = "Formato de teléfono no valido")]

        public string Telefono { get; set; }
		
        [Display(Name= "Correo Electrónico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El correo es obligatorio")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Dirección de mail Inválida")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string CorreoElectronico { get; set; }
		
        [Display(Name= "Nombre Director")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre Director es obligatorio")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Nombre Director")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]

        public string NombreDirector { get; set; }
		
        [Display(Name= "Teléfono Director")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El teléfono es obligatorio")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [RegularExpression(@"^[0-9 ]+$", ErrorMessage = "Formato de teléfono no valido")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string TelefonoDirector { get; set; }
		
        [Display(Name="Correo Director")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El correo es obligatorio")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Dirección de mail Inválida")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string CorreoDirector { get; set; }
		
        [Display(Name= "Sitio Web")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string SitioWeb { get; set; }
		
    
        public virtual ICollection<tblGrupoInvestigacion> tblGrupoInvestigacion { get; set; }
        public virtual ICollection<tblHojaVida> tblHojaVida { get; set; }
        public virtual tblMunicipios tblMunicipios { get; set; }
        public virtual tblTipoInstitucion tblTipoInstitucion { get; set; }
        public virtual tblZona tblZona { get; set; }
        public virtual ICollection<tblMaestroCoInvestigador> tblMaestroCoInvestigador { get; set; }
        public virtual ICollection<tblTutorZona> tblTutorZona { get; set; }
    }
}