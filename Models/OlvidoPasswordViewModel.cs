using System.ComponentModel.DataAnnotations;

namespace xilopro2.Models
{
    public class OlvidoPasswordViewModel
    {

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }

    }
}
