using System.ComponentModel.DataAnnotations;

namespace xilopro2.Data.Entities
{
    public class GroupDetail
    {
        [Key]
        public int GroupDetail_ID { get; set; }


        [Display(Name = "Partidos Jugados")]
        public int MatchesPlayed { get; set; }

        [Display(Name = "Victorias")]
        public int MatchesWon { get; set; }

        [Display(Name = "Empates")]
        public int MatchesTied { get; set; }

        [Display(Name = "Derrotas")]
        public int MatchesLost { get; set; }

        public int Points => MatchesWon * 3 + MatchesTied;

        [Display(Name = "Goles a a Favor")]
        public int GoalsFor { get; set; }

        [Display(Name = "Goles en contra")]
        public int GoalsAgainst { get; set; }

        [Display(Name = "Diferencia de Goles")]
        public int GoalDifference => GoalsFor - GoalsAgainst;





        //relations
        public Groups Groups { get; set; }
        public Team Team { get; set; }
    }
}
