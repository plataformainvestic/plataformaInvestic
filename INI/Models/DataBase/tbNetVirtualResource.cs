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
    public partial class tbNetVirtualResource
    {
        [Display(Name="id")]
		[Required]
		[Range(0,int.MaxValue)]
		public int id { get; set; }
		
        [Display(Name="idNetVirtualUser")]
		[Required]
		public System.Guid idNetVirtualUser { get; set; }
		
        [Display(Name="resource")]
		[DataType(DataType.Upload)]
		public byte[] resource { get; set; }
		
        [Display(Name="JsonMetadata")]
		public string JsonMetadata { get; set; }
		
        [Display(Name="name")]
		public string name { get; set; }
		
        [Display(Name="description")]
		public string description { get; set; }
		
        [Display(Name="idCategory")]
		[Range(0,int.MaxValue)]
		public Nullable<int> idCategory { get; set; }
		
    
        public virtual tbNetVirtualUser tbNetVirtualUser { get; set; }
        public virtual tbNetVirtualCategoryResource tbNetVirtualCategoryResource { get; set; }
    }
}