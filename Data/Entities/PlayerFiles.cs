using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace xilopro2.Data.Entities
{
    public class PlayerFiles
    {
        [Key]
        public int PlayerFiles_ID { get; set; }

        [Display(Name = "Nombre:", Prompt = "Nombre de registro")]
        public string PlayerFiles_Name { get; set; }


        [Display(Name = "Imagen de Archivo:", Prompt = "Foto")]
        public string? PlayerFiles_Image { get; set; }

        [NotMapped]
        [Display(Name = "Escoger Imagen:")]
        public IFormFile FotoFile { get; set; }


        //a uno
        // Propiedad de navegación inversa
        public int PlayerId { get; set; }
        public Player? Player { get; set; }


    }
}
