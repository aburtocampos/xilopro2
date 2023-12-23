using System.ComponentModel.DataAnnotations;
using xilopro2.Data.Entities;

namespace xilopro2.Models
{
    public class TorneoViewModel:Torneo
    {
        [Display(Name = "Logo:")]
        public IFormFile? LogoFile { get; set; }

    }
}
