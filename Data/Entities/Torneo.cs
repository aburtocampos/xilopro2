using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace xilopro2.Data.Entities
{
    public class Torneo
    {
        [Key]
        public int Torneo_ID { get; set; }

        [Display(Name = "Torneo:")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Torneo_Name { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Inicio:")]
        [DisplayFormat(DataFormatString = "{0:yyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Torneo_StartDate { get; set; }


        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Inicio:")]
        [DisplayFormat(DataFormatString = "{0:yyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Torneo_StartDateLocal => Torneo_StartDate.ToLocalTime();

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Finalizacion:")]
        [DisplayFormat(DataFormatString = "{0:yyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Torneo_EndDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Finalizacion:")]
        [DisplayFormat(DataFormatString = "{0:yyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Torneo_EndDateLocal => Torneo_EndDate.ToLocalTime();


        [Display(Name = "Temporada:")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Torneo_Season { get; set; }

        [Display(Name = "Imagen:")]
        public string? Torneo_Image { get; set; }

        [Display(Name = "Estado:")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool Torneo_Status { get; set; }

        [Display(Name = "Categorias:")]
        public List<int>? SelectedCategoryIds { get; set; }


        //relations
        public ICollection<Groups>? Groups { get; set; }
        public ICollection<Player>? Players { get; set; }

    }
}
