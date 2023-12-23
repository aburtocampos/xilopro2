using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xilopro2.Data.Entities
{
    public class Parent
    {
        [Key]
        public string Parent_ID { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Parent_FirstName { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Parent_LastName { get; set; }

        [Display(Name = "Cedula:")]
        [RegularExpression(@"[0-9]{3}[0-9]{6}[0-9]{4}[A-Z]{1}", ErrorMessage = "Formato de Cedula incorrecto")]
        [MaxLength(14, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Parent_Cedula { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(8, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Dirección:")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Parent_Address { get; set; }


        [Display(Name = "Foto:", Prompt = "Foto")]
        public string? Parent_Image { get; set; }

        [NotMapped]
        [Display(Name = "Escoger Imagen:")]
        public IFormFile FotoFile { get; set; }


        [Display(Name = "Foto Cedula:", Prompt = "Foto")]
        public string? Parent_ImageCedula { get; set; }

        [NotMapped]
        [Display(Name = "Escoger Imagen:")]
        public IFormFile FotoFileCedula { get; set; }


        [Display(Name = "Tutor:")]
        public string Parent_FullName => $"{Parent_FirstName} {Parent_LastName}";



        // Propiedad de navegación inversa
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int CountryID { get; set; } = 0;

        public int StateID { get; set; } = 0;

        public int CityID { get; set; } = 0;




    }
}
