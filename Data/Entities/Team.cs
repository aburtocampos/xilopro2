using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace xilopro2.Data.Entities
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Team_ID { get; set; }

        [Display(Name = "Nombre:", Prompt = "Nombre del Equipo")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Team_Name { get; set; }

        [Display(Name = "Estadio:", Prompt = "Estadio del Equipo")]
        public string? Team_Estadio { get; set; }

        [Display(Name = "Logo:", Prompt = "Logo del Equipo")]
        public string? Team_Image { get; set; }

        //relations
        //  public ICollection<AppUser> AppUsers { get; set; }
        //  public int? userid { get; set; }
        //   public User? User { get; set; }
        public ICollection<Player>? Players { get; set; }
        public ICollection<Match> Matches { get; set; }

    }
}
