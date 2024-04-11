using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace xilopro2.Data.Entities
{
    public class Membership
    {
        [Key]
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
        public string MembershipType { get; set; }

        [Display(Name = "Fecha de Inicio:")]
        [BindProperty, DataType("month")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Fecha de Finalización:")]
        [BindProperty, DataType("month")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Estado:")]
        public string Status { get; set; }

        [Display(Name = "Miembro")]
        public string? Membership_FullName { get; set; }

        [Display(Name = "Pagos")]
        public int PaymentsNumber => Payments == null ? 0 : Payments.Count;


        public List<Payments>? Payments { get; set; }


    }
}
