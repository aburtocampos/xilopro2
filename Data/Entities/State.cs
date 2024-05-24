using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xilopro2.Data.Entities
{
    public class State
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int State_ID { get; set; }

        [Display(Name = "Departamento")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string State_Name { get; set; }


        public int? CountryId { get; set; }
        //a uno
        [ForeignKey("CountryId")]
        //[ValidateNever]
        public Country Country { get; set; }

        //de muchos
        public virtual ICollection<City> Cities { get; set; }

      
        [Display(Name = "Municipios")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
    }
}
