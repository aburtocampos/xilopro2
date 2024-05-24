using System.ComponentModel.DataAnnotations;
using xilopro2.Data.Entities;

namespace xilopro2.Models
{
    public class CorrectActionViewModel
    {
       
        public int CorrectionAction_ID { get; set; }

        [Display(Name = "Accion Correctiva:")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string CorrectionAction_Name { get; set; }

        [Display(Name = "Estado:")]
        public bool CorrectionAction_Status { get; set; }


        [Display(Name = "Descripción:")]
        public string Description { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Fecha { get; set; }

        public int PlayerId { get; set; }


        [Display(Name = "Jugador:")]
        public virtual string PlayerName { get; set; }


    }
}
