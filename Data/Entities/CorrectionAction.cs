using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using xilopro2.Models;

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

        [Display(Name = "Descripción:")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Fecha { get; set; }


        [Display(Name = "Jornadas a Suspender:")]
        public List<string>? Jornadasasancionar { get; set; }

        public int? groupId { get; set; }

        public int PlayerId { get; set; } = 0;

        [Display(Name = "Jugador:")]
        public virtual string PlayerName { get; set; } = string.Empty;


        public Player? Player { get; set; }
    }
}
