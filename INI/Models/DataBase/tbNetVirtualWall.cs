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
    public partial class tbNetVirtualWall
    {
        [Display(Name="id")]
		[Required]
		[Range(0,int.MaxValue)]
		public int id { get; set; }
		
        [Display(Name="message")]
		public string message { get; set; }
		
        [Display(Name="idNetVirtualUserGroup")]
		[Range(0,int.MaxValue)]
		public Nullable<int> idNetVirtualUserGroup { get; set; }
		
        [Display(Name="dateSend")]
		public Nullable<System.DateTime> dateSend { get; set; }
		
    
        public virtual tbNetVirtualUserGroup tbNetVirtualUserGroup { get; set; }
    }
}
