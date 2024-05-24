using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using xilopro2.Data.Entities;

namespace xilopro2.Models
{
    public class GroupDetailViewModel
    {


        public int GroupDetailID { get; set; }


        [Display(Name = "Partidos Jugados")]
        public int MatchesPlayed { get; set; }

        [Display(Name = "Victorias")]
        public int? MatchesWon { get; set; } = 0;

        [Display(Name = "Empates")]
        public int? MatchesTied { get; set; } = 0;

        [Display(Name = "Derrotas")]
        public int? MatchesLost { get; set; } = 0;

        public int? Points => MatchesWon * 3 + MatchesTied;

        [Display(Name = "Goles a a Favor")]
        public int GoalsFor { get; set; }

        [Display(Name = "Goles en contra")]
        public int GoalsAgainst { get; set; }

        [Display(Name = "Diferencia de Goles")]
        public int GoalDifference => GoalsFor - GoalsAgainst;



        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public string? TeamName { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Equipo")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a team.")]
        public int TeamId { get; set; }

        public IEnumerable<SelectListItem>? Teams { get; set; }

    }


}
