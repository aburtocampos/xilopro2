using xilopro2.Data.Entities;

namespace xilopro2.Models
{
    public class AddPlayersToTorneoViewModel
    {

        public int id { get; set; }


       // public int playerid { get; set; }

        public string season { get; set; }

        public int torneoid { get; set; }

        public ICollection<Player> Players { get; set; }

        public ICollection<Player> PlayersTorneo { get; set; }

        public Torneo? Torneo { get; set; }

        public ICollection<Groups> Grupos { get; set; }
    }
}
