using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace xilopro2.Data.Entities
{
    public class Player
    {

        [Key]
        public int Player_ID { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Player_FirstName { get; set; }

        [Display(Name = "Apellido")]
        public string Player_LastName { get; set; }

        [Display(Name = "Dorsal")]
        [Range(1, 30, ErrorMessage = "El campo {0} debe tener un valor maximo de {1} y un minimo de {2}")]
        public int Player_Dorsal { get; set; } = 0;

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Player_FNC { get; set; }

        [NotMapped] 
        [Display(Name = "Edad:")]
        public int Player_Edad

        {
            get
            {
                if (Player_FNC == null)
                {
                    return 0; // Puedes manejar el caso de FechaNacimiento nula como desees.
                }

                var today = DateTime.Today;
                var age = today.Year - Player_FNC.Year;

                if (Player_FNC.Date > today.AddYears(-age))
                {
                    age--; // Ajustar si aún no ha cumplido años este año.
                }

                return age;
            }
        }

        [Display(Name = "Teléfono:")]
        [MaxLength(8, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string Player_PhoneNumber { get; set; }

        [Display(Name = "Dirección:")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Player_Address { get; set; }


        [Display(Name = "Estado")]
        public bool Player_Status { get; set; }


        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Debes introducir un email válido.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Player_Email { get; set; }


        [Display(Name = "Cedula:")]
        [RegularExpression(@"[0-9]{3}[0-9]{6}[0-9]{4}[A-Z]{1}", ErrorMessage = "Formato de Cedula incorrecto")]
        [MaxLength(14, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? Player_Cedula { get; set; }


        [Display(Name = "Genero:")]
        public string Player_Genero { get; set; }

        [Display(Name = "FIFAID:", Prompt = "FIFAID")]
        [MaxLength(5, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        public string? Player_fifaid { get; set; }

 
        [Display(Name = "Foto:", Prompt = "Foto")]
        public string? Player_Image { get; set; }

        [NotMapped]
        [Display(Name = "Escoger Imagen:")]
        public IFormFile FotoFile { get; set; }


        [Display(Name = "Jugador")]
        public string Player_FullName => $"{Player_FirstName} {Player_LastName}";

        public string Player_UserRol => "User";

        [Display(Name = "Categorias:")]
        public List<int> SelectedCategoryIds { get; set; }





        //relations


        public Team? Team { get; set; }

       public Position? Position { get; set; }



        public int Countryid { get; set; }

         public Country? Country { get; set; }

         public int Stateid { get; set; }

         public int Cityid { get; set; }



        public List<Parent>? Parents { get; set; }

        public List<PlayerFiles>? PlayerFiles { get; set; }



    }
}
