using System.ComponentModel.DataAnnotations;

namespace xilopro2.Models
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress]

        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(50, ErrorMessage = "El {0} debe estar entre al menos {2} caracteres de longitud", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña:")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmación es requerida")]
        [Compare("Password", ErrorMessage = "La contraseñá y la confirmación de contraseña no coinsiden")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña:")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }

    }
}
