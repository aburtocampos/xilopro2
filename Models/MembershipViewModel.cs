using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace xilopro2.Models
{
    public class MembershipViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Nombres:")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string MemberFirstName { get; set; }

        [Display(Name = "Apellidos:")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string MemberLastName { get; set; }

        [Display(Name = "Tipo de Membresia:")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string MembershipType { get; set; }

        [Display(Name = "Fecha de Inicio:")]
        [BindProperty, DataType("month", ErrorMessage = "Your message here")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Fecha de Finalización:")]
        [BindProperty, DataType("month")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Estado:")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Status { get; set; }

        [Display(Name = "Miembro")]
        public string? Membership_FullName { get; set; }

    }
}
