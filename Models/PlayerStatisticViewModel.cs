using System.ComponentModel.DataAnnotations;
using xilopro2.Data.Entities;

namespace xilopro2.Models
{
    public class PlayerStatisticViewModel
    {
        public int PlayerStatistic_ID { get; set; }
        public int PlayerId { get; set; }
        public string? PlayerName { get; set; }

        [Display(Name = "Goles:")]
        public int Goals { get; set; }

        [Display(Name = "Tarjetas Amarillas:")]
        public int YellowCards { get; set; }

        [Display(Name = "Faltas:")]
        public int Fouls { get; set; }

        [Display(Name = "Tarjetas Rojas:")]
        public int RedCards { get; set; }

        [Display(Name = "Goles Atajados:")]
        public int GoalkeeperSaves { get; set; }

        [Display(Name = "Goles Permitidos:")]
        public int GoalsConceded { get; set; }

        [Display(Name = "Penaltis:")]
        public int Penalties { get; set; }

        [Display(Name = "Tiros de Esquina:")]
        public int CornerKicks { get; set; }



        public int MatchId { get; set; }
        public int DetailsGroupId { get; set; }
        public int TorneoId { get; set; }
        public List<Player>? Players { get; set; }

    }
}
