using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace xilopro2.Models
{
    public class MembershipViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Nombres:")]
        public string MemberFirstName { get; set; }

        [Display(Name = "Apellidos:")]
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

    }
}
