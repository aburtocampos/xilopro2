using System.ComponentModel.DataAnnotations;
using xilopro2.Data.Entities;
namespace xilopro2.Models
{
    public class TablaGeneralViewModel
    {
        public List<xilopro2.Data.Entities.Groups>? Groups { get; set; }
        public List<xilopro2.Data.Entities.Matchgame>? Matches { get; set; }

        public int torneoid { get; set; }
    }
}
