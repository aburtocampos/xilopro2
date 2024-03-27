using System.ComponentModel.DataAnnotations;

namespace xilopro2.Data.Entities
{
    public class PlayerStatistics
    {
       
       [Key]
        public int PlayerStatistic_ID { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int MatchId { get; set; }

        public Matchgame Matchgames { get; set; }


        public int Goals { get; set; }
        public int YellowCards { get; set; }
        public int Fouls { get; set; }
        public int RedCards { get; set; }
        public int GoalkeeperSaves { get; set; }
        public int GoalsConceded { get; set; }
        public int Penalties { get; set; }
        public int CornerKicks { get; set; }
    }
}
