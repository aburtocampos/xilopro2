using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace xilopro2.Data.Entities
{
    public class Category
    {
  
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Category_ID { get; set; }

        [Display(Name = "Categoria:")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Category_Name { get; set; }

        [Display(Name = "Estado:")]
        public bool Category_Status { get; set; }

        // Colección de Navegación
      //  public ICollection<Player> Players { get; set; }

    }
}
