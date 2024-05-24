using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xilopro2.Data.Entities
{
    public class Country
    {
        [Key]
        public int Country_ID { get; set; }

        [Display(Name = "País")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Country_Name { get; set; }

        //de muchos
        public virtual ICollection<State> States { get; set; }

        public virtual ICollection<Player>? Players { get; set; }

        [Display(Name = "Departamentos")]
        public int StatesNumber => States == null ? 0 : States.Count;

    }
}
