using System.ComponentModel.DataAnnotations;

namespace xilopro2.Data.Entities
{
    public class State
    {
        [Key]
        public int State_ID { get; set; }

        [Display(Name = "Departamento")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string State_Name { get; set; }

        //a uno
        public Country Country { get; set; }

        //de muchos
        public virtual ICollection<City> Cities { get; set; }

      
        [Display(Name = "Municipios")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
    }
}
