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


        //drops


        /*  [Display(Name = "Pais:")]
          [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un pais.")]
          [Required(ErrorMessage = "El campo {0} es obligatorio.")]
          public int CountryID { get; set; } = 0;



          [Display(Name = "Departamento:")]
          [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un departamento.")]
          [Required(ErrorMessage = "El campo {0} es obligatorio.")]
          public int StateID { get; set; } = 0;



          [Display(Name = "Municipio:")]
          [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un departamento.")]
          [Required(ErrorMessage = "El campo {0} es obligatorio.")]
          public int CityID { get; set; } = 0;*/
     

        public IEnumerable<SelectListItem>? Countries { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }


        /*    [Display(Name = "Categoria:")]
            [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoria.")]
            [Required(ErrorMessage = "El campo {0} es obligatorio.")]
            public int Categoryid { get; set; } = 0;

            public IEnumerable<SelectListItem> Categories { get; set; }*/

        public IEnumerable<SelectListItem>? Categories { get; set; }

        [Display(Name = "Categorias:")]
        public List<int> SelectedCategoryIdss { get; set; }


        [Display(Name = "Equipo:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un equipo.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Teamid { get; set; } = 0;
        public IEnumerable<SelectListItem>? Teams { get; set; }


        [Display(Name = "Posición:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un equipo.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Positionid { get; set; } = 0;
        public IEnumerable<SelectListItem>? Positions { get; set; }

      

    }
}
