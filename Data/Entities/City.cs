using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xilopro2.Data.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int City_ID { get; set; }

        [Display(Name = "Municipio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string City_Name { get; set; }

     
        public int? IdState { get; set; }

        [ForeignKey("IdState")]
        public State State { get; set; }

    }
}
