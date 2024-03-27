using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace xilopro2.Data.Entities
{
    public class Groups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Group_ID { get; set; }

        [Display(Name = "Nombre de Grupo:")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Group_Name { get; set; }


        [Display(Name = "Tipo")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string? Group_Type { get; set; }


        //relaciones

        public int torneoId { get; set; }
        public  Torneo Torneo { get; set; }

        public  ICollection<GroupDetail> GroupDetails { get; set; }

        public  ICollection<Matchgame> Matches { get; set; }
    }
}
