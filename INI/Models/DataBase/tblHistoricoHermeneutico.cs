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
    public partial class tblHistoricoHermeneutico
    {
        public tblHistoricoHermeneutico()
        {
            this.tblMetodoProy = new HashSet<tblMetodoProy>();
        }
    
        [Display(Name= "Histórico Hermenéutico Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Histórico Hermenéutico Id es obligatorio")]
        [Range(0,int.MaxValue)]
		public int tblHistoricoHermeneutico_ID { get; set; }
		
        [Display(Name= "Histórico Hermenéutico Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Histórico Hermenéutico Nombre es obligatorio")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Histórico Hermenéutico Nombre")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]

        public string hisHerm_nombre { get; set; }
		
    
        public virtual ICollection<tblMetodoProy> tblMetodoProy { get; set; }
    }
}
