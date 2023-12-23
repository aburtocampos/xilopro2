using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace xilopro2.Data.Entities
{
    public class Groups
    {
        [Key]
        public int Group_ID { get; set; }

        [Display(Name = "Grupo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Group_Name { get; set; }


        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Group_Type { get; set; }


        //relaciones
        public virtual ICollection<Torneo> Torneos { get; set; }

        public virtual ICollection<GroupDetail> GroupDetails { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}
