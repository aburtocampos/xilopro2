namespace xilopro2.Models
{
    public class PlayerStatisticViewModel
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }


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
