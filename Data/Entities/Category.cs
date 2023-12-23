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

      //  public ICollection<Player> Players { get; } = new List<Player>();

        //  public virtual ICollection<IdentityUserCategoria> IdentityUserCategorias { get; set; } 

        //   public virtual ICollection<Player> Players { get; set; }
        //

        /*  public string UserId { get; set; } = string.Empty;

          public virtual User User { get; set; }*/

        // public virtual ICollection<UserCategory>? UserCategory { get; set; }
        // public virtual ICollection<UserCategory> UserCategory { get; set; }

    }
}
