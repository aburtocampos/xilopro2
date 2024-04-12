using System.ComponentModel.DataAnnotations;

namespace xilopro2.Data.Entities
{
    public class Payments
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime PaymentDate { get; set; }


        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal PaymentAmount { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string PaymentStatus { get; set; }

        public int MembershipId { get; set; }
        public Membership? Membership { get; set; }


    }
}
