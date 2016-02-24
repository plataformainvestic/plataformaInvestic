using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;


namespace INI.Models
{
    public class Reporte
    {
        [Required]
        public DateTime Fechainicial { get; set; }

        [Required]
        public DateTime FechaFinal { get; set; }

        [Required]
        public int Tipo { get; set; }

        public string UserId { get; set; }
    }
}