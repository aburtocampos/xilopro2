using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;

namespace xilopro2.Data.Entities
{
    public class AppUser:IdentityUser
    {


        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? User_FirstName { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? User_LastName { get; set; }

        [Display(Name = "Dirección")]
        [DataType(DataType.MultilineText)]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? User_Address { get; set; }

        [Display(Name = "Cedula:")]
        [RegularExpression(@"[0-9]{3}[0-9]{6}[0-9]{4}[A-Z]{1}", ErrorMessage = "Formato de Cedula incorrecto")]
        [MaxLength(14, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? User_Cedula { get; set; } = string.Empty;

        [Display(Name = "Genero:")]
        public string? User_Genero { get; set; }

        [Display(Name = "Estado")]
        public bool User_Status { get; set; }

        [Display(Name = "Creado en")]
        public DateTime User_CreatedTime { get; set; }


        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime User_FNC { get; set; }


        [Display(Name = "Teléfono")]
        [MaxLength(8, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

     

        [Display(Name = "Foto:", Prompt = "Foto")]
        public string? User_Image { get; set; }


        [NotMapped]
        [Display(Name = "Escoger Imagen:")]
        public IFormFile FotoFile { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string UserTypeofRole { get; set; }


        [Display(Name = "Nombres")]
        public string User_FullName => $"{User_FirstName} {User_LastName}";


        [Display(Name = "Categorias:")]
        public List<int>? SelectedCategoryIds { get; set; }





        [Display(Name = "Pais:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un pais.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int? Countryid { get; set; } = 0;

        [Display(Name = "Departamento:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una departamento.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int? Stateid { get; set; } = 0;

        [Display(Name = "Municipio:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un municipio.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int? Cityid { get; set; } = 0;



        /*   public int[] Cateroriaids { get; set; }

            [Display(Name = "Categoria")]
            [Required(ErrorMessage = "El campo {0} es obligatorio.")]
            public Category? Category { get; set; }*/
        //  [Display(Name = "Categorias:")]
        // public List<Category>? Categories { get; set; } = new();

        //relations




      //  public int? Team_ID { get; set; }
      //  public Team? Team { get; set; }


      //  public int? Position_ID { get; set; }
//public Position? Position { get; set; }
      // public virtual ICollection<Position>? Positions { get; set; }

        //de muchos


        //  [Display(Name = "# de Partidos")]
        //   public int MatchCount => Matchs == null ? 0 : Matchs.Count;

        //   public ICollection<Match> Matchs { get; set; }




        //    public virtual ICollection<IdentityUserCategoria> IdentityUserCategorias { get; set; }

        //  public virtual ICollection<UserCategory> UserCategory { get; set; }
        //  public virtual ICollection<Category> Categories { get; set; }

        // public virtual ICollection<UserCategory> UserCategory { get; set; }
    }
}
