using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INI.Models.DataBase;
using System.ComponentModel.DataAnnotations;

namespace INI.Models.BusquedaInvestigacion
{
    public class SearchProyect
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El titulo es obligatorio")]
        [Display(Name = "Titulo", Prompt = "Titulo")]
        [RegularExpression(@"^[a-zA-Z0-9 ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Titulo")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]

        public string Titulo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Palabras Clave es obligatorio")]
        [Display(Name = "PalabrasClave", Prompt = "PalabrasClave")]
                           
        [RegularExpression(@"^[a-zA-Z0-9 ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Palabras Clave")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]

        public string PalabrasClave { get; set; }

        public int EsProyectoInvestigacion { get; set; }

        public int tblEjeInvestigacion_ID { get; set; }
        public int EsBusquedaAvanzada { get; set; }

        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El autor es obligatorio")]
        [Display(Name = "Autor", Prompt = "Autor")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para autor")]
        public string Autor { get; set; }

    }
}