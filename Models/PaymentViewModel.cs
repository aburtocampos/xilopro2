using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace xilopro2.Models
{
    public class PaymentViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Fecha de pago:")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Monto:")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal PaymentAmount { get; set; }

        [Display(Name = "Metodo de pago:")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Estado del pago:")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string PaymentStatus { get; set; }

        public int MembersId { get; set; }

        public virtual string? MembershipFullName { get; set; }
    }
}
