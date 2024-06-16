using xilopro2.Data.Entities;

namespace xilopro2.Models
{
    public class PlayerStatisticReportViewModel
    {
        public PlayerStatistics PlayerStatistics { get; set; }
        public Torneo Torneo { get; set; }
        public Matchgame Matches{ get; set; }
        public Team Teams { get; set; }
    }
}
