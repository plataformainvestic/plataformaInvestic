using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INI.Models.RangeFechas
{
    public class DateRange
    {

        [Display(Name = "Fecha inicial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        [CustomValidation(typeof(ValidationDataRange), "IsValid")]//,ErrorMessage ="Error en la fecha")]
        
        public DateTime StartDate { get; set; }

        [Display(Name = "Fecha final")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        [CustomValidation(typeof(ValidationDataRange), "IsValid")]//,ErrorMessage ="Error en la fecha")]
        
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate <= StartDate)
            {
                yield return new ValidationResult("La fecha final debe ser mayor o igual que la final");
            }
        }
    }

    public class ReportModel : DateRange
    {
        public bool IsRole { get; set; }
        public bool IsAdvancedSearch { get; set; }

        public string criterion { get; set; }
        
    }

    public static class ValidationDataRange
    {
        public static ValidationResult IsValid(DateTime value)
        {
           

            if (value <= DateTime.Now)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("La fecha deber ser inferior a la actual");
        }
    }
   
}