using System.ComponentModel.DataAnnotations;

namespace xilopro2.Models
{
    public class PlayersGoals
    {
        public int PlayerId { get; set; }

        [Display(Name = "Jugador")]
        public string Player_FullName { get; set; }

        [Display(Name = "Dorsal")]
        public int Player_Dorsal { get; set; }

        [Display(Name = "Foto")]
        public string? Player_Image { get; set; }

        [Display(Name = "Goles")]
        public int Goals { get; set; }

        [Display(Name = "Equipo")]
        public string? Team_Image { get; set; }


        public string? Team_Name { get; set; }


        public int? torneoid { get; set; }
    }
}
