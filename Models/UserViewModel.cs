using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xilopro2.Models
{
    public class UserViewModel
    {

        public string Id { get; set; }

        [Display(Name = "Email:")]
        [EmailAddress(ErrorMessage = "Debes introducir un email válido.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Email { get; set; }

        [Display(Name = "Nombre:")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string User_FirstName { get; set; }

        [Display(Name = "Apellido:")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string User_LastName { get; set; }


        [Display(Name = "Dirección:")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string User_Address { get; set; }

        [Display(Name = "Teléfono:")]
        [MaxLength(8, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime User_CreatedTime { get; set; }

        [Display(Name = "Cedula:")]
        [RegularExpression(@"[0-9]{3}[0-9]{6}[0-9]{4}[A-Z]{1}", ErrorMessage = "Formato de Cedula incorrecto")]
        [MaxLength(14, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string User_Cedula { get; set; }

        [Display(Name = "Estado del Usuario:")]
        public bool Status { get; set; }

        [Display(Name = "Imagen de Pérfil:")]
        public IFormFile? FotoFile { get; set; }

        public string? User_Image { get; set; }


        [Display(Name = "Rol:")]
        public string UserTypeof { get; set; } = string.Empty;

        [Display(Name = "Genero:")]
        public string User_Genero { get; set; }


        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime User_FNC { get; set; }


        [Display(Name = "Pais:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un pais.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Countryid { get; set; } = 0;

        [Display(Name = "Departamento:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una departamento.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Stateid { get; set; } = 0;

        [Display(Name = "Municipio:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un municipio.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Cityid { get; set; } = 0;

        [NotMapped] // Esta propiedad no se mapea a una columna en la base de datos.
        [Display(Name = "Edad:")]
        public int User_Edad

        {
            get
            {
                if (User_FNC == null)
                {
                    return 0; // Puedes manejar el caso de FechaNacimiento nula como desees.
                }

                var today = DateTime.Today;
                var age = today.Year - User_FNC.Year;

                if (User_FNC.Date > today.AddYears(-age))
                {
                    age--; // Ajustar si aún no ha cumplido años este año.
                }

                return age;
            }
        }
        /*  [Display(Name = "Categoria:")]
          [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoria.")]
          [Required(ErrorMessage = "El campo {0} es obligatorio.")]
          public int? Categoryid { get; set; } = 0;*/

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Categorias:")]
        public List<int> SelectedCategoryIds { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }



        [Display(Name = "Roles:")]
        public IEnumerable<SelectListItem> UserType { get; set; }

    }
}
