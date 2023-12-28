using System.ComponentModel.DataAnnotations;

namespace xilopro2.Data.Entities
{
    public class CorrectionAction
    {
        [Key]
        public int CorrectionAction_ID { get; set; }

        [Display(Name = "Accion Correctiva:")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string CorrectionAction_Name { get; set; }

        [Display(Name = "Estado:")]
        public bool CorrectionAction_Status { get; set; }




        public ICollection<Player> Players { get; set; }
    }
}
