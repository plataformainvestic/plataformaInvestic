using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;

namespace MAI.Models
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
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coincide.")]
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
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Usuario", Prompt = "Usuario")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña", Prompt = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña", Prompt = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coincide.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage="El nombre es un campo obligatorio")]        
        [Display(Name = "Nombres", Prompt = "Nombres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido es un campo obligatorio")]
        [Display(Name = "Apellidos", Prompt = "Apellidos")]
        public string SureName { get; set; }

        [Required(ErrorMessage = "El documento es un campo obligatorio")]
        [Display(Name = "Documento", Prompt = "Documento")]
        public string PersonalID { get; set; }

        [Display(Name = "Genero")]        
        public int Genre { get; set; }

        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }        

        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Introduzca una dirección de mail válida")]
        public string Mail { get; set; }

        [Display(Name = "Direccion")]        
        public string Address { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime BirthDay { get; set; }

    }    
}
