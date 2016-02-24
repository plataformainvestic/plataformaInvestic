using System.ComponentModel.DataAnnotations;
using System;

namespace SEG.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contreseña actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Por favor ingrese Numero de Ceula")]
        [Display(Name = "Cedula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Por favor ingrese Contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordarme?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El numero de cedula es un campo obligatorio")]
        [Display(Name = "Cedula")]
        [StringLength(10)]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Por favor ingrese Nombres")]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Por favor ingrese Apellidos")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Por favor ingrese Numero de Celular")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña", Prompt = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña", Prompt = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Por favor ingrese una dirección de correo electrónico válida")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        
        
        [Display(Name = "Cargo")]
        public string Cargo { get; set; }

       
        [Display(Name = "Nro. de Contrato")]
        public string Contrato { get; set; }


       
        public DateTime? Fecha_IniContrato { get; set; }
        
        
        public DateTime? Fecha_FinContrato { get; set; }

       
        [Display(Name = "No. CDP")]
        public string Cdp { get; set; }

       
        [Display(Name = "Equipo")]
        public string Equipo { get; set; }

        [Display(Name = "Rol")]
        public string Rol { get; set; }
    }
}