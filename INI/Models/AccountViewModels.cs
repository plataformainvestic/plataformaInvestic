using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace INI.Models
{    

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Constraseña actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario", Prompt = "Usuario")]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }



        [Display(Name = "Recordarme?")]
        public bool RememberMe { get; set; }


        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Altitud { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Usuario", Prompt = "Usuario")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Contraseña Obligatoria")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña", Prompt = "Contraseña")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Contraseña Obligatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña", Prompt = "Confirmar Contraseña")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Las contraseñas no coincide.")]
        public string ConfirmPassword { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombres", Prompt = "Nombres")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Nombres")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El apellido es obligatorio")]
        [Display(Name = "Apellidos", Prompt = "Apellidos")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Carácter no valido para Apellidos")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string SureName { get; set; }
        

        [Display(Name = "Tipo de Documento")]
        public int TipoDoc { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El documento es obligatorio")]
        [Display(Name = "Documento", Prompt = "Documento")]
        [RegularExpression(@"^[a-zA-Z0-9 ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Caractér no valido en documento")]
        public string PersonalID { get; set; }

        [Display(Name = "Genero")]        
        public int Genre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El teléfono es obligatorio")]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [RegularExpression(@"^[0-9 ]+$", ErrorMessage = "Formato de teléfono no valido")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El correo es obligatorio")]
        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Dirección de mail Inválida")]

        public string Mail { get; set; }

        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        [DataType(DataType.Date, ErrorMessage = "Fecha nacimiento es obligatoria")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        [CustomValidation(typeof(ValidacionFechaBirthDay), "IsValid",ErrorMessage ="Error en fecha")]
        public DateTime BirthDay { get; set; }

    }

    public class RegisterViewModelUpdate
    {


        public string Id { get; set; }

        [Required]
        [Display(Name = "Usuario", Prompt = "Usuario")]
        public string UserName { get; set; }


        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña", Prompt = "Contraseña")]
        public string Password { get; set; }




        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña", Prompt = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña", Prompt = "Confirmar Contraseña")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Las contraseñas no coincide.")]
        public string ConfirmPassword { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombres", Prompt = "Nombres")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Caractéres No valido para Nombres")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El apellido es obligatorio")]
        [Display(Name = "Apellidos", Prompt = "Apellidos")]
        [RegularExpression(@"^[a-zA-Z ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Caractéres No valido para Apellidos")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string SureName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El documento es obligatorio")]
        [Display(Name = "Documento", Prompt = "Documento")]
        [RegularExpression(@"^[a-zA-Z0-9 ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Caractéres No valido para Documento")]
        public string PersonalID { get; set; }

        [Display(Name = "Genero")]
        public int Genre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Telefono es obligatorio")]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [RegularExpression(@"^[0-9 ]+$", ErrorMessage = "Formato de telefono No valido")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Correo es obligatorio")]
        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Dirección de mail válida")]

        public string Mail { get; set; }

        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        [DataType(DataType.Date, ErrorMessage = "Fecha Nacimiento es obligatoria")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        [CustomValidation(typeof(ValidacionFechaBirthDay), "IsValid")]//,ErrorMessage ="Error en la fecha")]
        public DateTime BirthDay { get; set; }
    }


    public class ResetPasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El documento es obligatorio")]
        [Display(Name = "Documento", Prompt = "Documento")]
        [RegularExpression(@"^[a-zA-Z0-9 ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Caractér no valido en documento")]
        public string PersonalID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
    public class ForgotPasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El documento es obligatorio")]
        [Display(Name = "Documento", Prompt = "Documento")]
        [RegularExpression(@"^[a-zA-Z0-9 ñáéíóúÁÉÍÓÚÑ]+$", ErrorMessage = "Caractér no valido en documento")]
        public string PersonalID { get; set; }
    }

    public static class ValidacionFechaBirthDay
    {
        public static ValidationResult IsValid(DateTime value)
        {

            //--03/09/2015<=  02/09/2010 && 03/09/2015>=  02/09/1910 No nace  false
            //--02/09/2013<=  02/09/2010 && 02/09/2013>=  02/09/1910 2años    false
            //--12/05/1990<=  02/09/2010 && 12/05/1990>=  02/09/1910 25 años  true
            //--01/01/1890<=  02/09/2010 && 01/01/1890>=  02/09/1910 125 años false

            if (value <= DateTime.Now.AddYears(-5) && value >= DateTime.Now.AddYears(-100))
            {
                return ValidationResult.Success;
            }



            return new ValidationResult("La fecha debe estar relacionada con una edad comprendida entre 5 y 100 años a la fecha actual");
        }
    }

}
