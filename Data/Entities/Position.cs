using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xilopro2.Data.Entities
{
    public class Position
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Position_ID { get; set; }

        [Display(Name = "Posición de Juego:")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Position_Name { get; set; }

        [Display(Name = "Estado:")]
        public bool Position_Status { get; set; }

        //  public int? userid { get; set; }

        //  public ICollection<AppUser> AppUsers { get; set; }
         public ICollection<Player>? Players { get; set; }
    }
}
