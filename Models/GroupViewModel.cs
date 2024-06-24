using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using xilopro2.Data.Entities;

namespace xilopro2.Models
{
    public class GroupViewModel
    {
       
        public int GroupID { get; set; }

        [Display(Name = "Nombre de Grupo:")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Group_Name { get; set; }

        public int Torneoid { get; set; }

        public virtual string TorneoName { get; set; }

        public Torneo? Torneo { get; set; }

    }


}
