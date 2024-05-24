using System.ComponentModel.DataAnnotations;

namespace xilopro2.Models
{
    public class PlayerFilesViewModel
    {

        public int PlayerFiles_ID { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string PlayerFiles_Name { get; set; }

        public string? PlayerFiles_Image { get; set; }

        [Display(Name = "Vista Previa:")]
        public IFormFile? FotoFile { get; set; }

        public int PlayerId { get; set; }

        public virtual string PlayerName { get; set; }

    }
}
