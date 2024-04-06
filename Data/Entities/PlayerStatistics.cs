using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xilopro2.Data.Entities
{
    public class PlayerStatistics
    {
       
       [Key]
        public int PlayerStatistic_ID { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int MatchId { get; set; }

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


        [NotMapped]
        public int DetailsGroupId { get; set; }

        [NotMapped]
        public int TorneoId { get; set; }

        [NotMapped]
        public List<Player> Players { get; set; }

   



    }
}
