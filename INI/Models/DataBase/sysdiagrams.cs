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
    public partial class sysdiagrams
    {
        [Display(Name="Nombre")]
		[Required]
		public string name { get; set; }
		
        [Display(Name="Principal Id")]
		[Required]
		[Range(0,int.MaxValue)]
		public int principal_id { get; set; }
		
        [Display(Name="Diagram_id")]
		[Required]
		[Range(0,int.MaxValue)]
		public int diagram_id { get; set; }
		
        [Display(Name="Version")]
		[Range(0,int.MaxValue)]
		public Nullable<int> version { get; set; }
		
        [Display(Name="Definition")]
		[DataType(DataType.Upload)]
		public byte[] definition { get; set; }
		
    }
}
