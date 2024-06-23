using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using xilopro2.Data.Entities;

namespace xilopro2.Models
{
    public class PlayerViewModel:Player
    {


        [Display(Name = "Escoger Imagen:")]
        public IFormFile? FotoFile { get; set; }

        [NotMapped]
        public bool isUserImgErased { get; set; }
  

        public IEnumerable<SelectListItem>? Countries { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }

        public IEnumerable<SelectListItem>? Categories { get; set; }

        [Display(Name = "Categorias:")]
        public List<int> SelectedCategoryIdss { get; set; }


        [Display(Name = "Equipo:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un equipo.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Teamid { get; set; } = 0;
        public List<Team>? Teams { get; set; }


        [Display(Name = "Posición:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un equipo.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Positionid { get; set; } = 0;
        public IEnumerable<SelectListItem>? Positions { get; set; }

      

    }
}
