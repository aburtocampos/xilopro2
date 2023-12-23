using System.ComponentModel.DataAnnotations;
using xilopro2.Data.Entities;

namespace xilopro2.Models
{
    public class TeamViewModel:Team
    {
        [Display(Name = "Imagen:", Prompt = "Imagen")]
        public IFormFile? LogoFile { get; set; }
    }
}
