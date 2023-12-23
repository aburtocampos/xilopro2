using System.ComponentModel.DataAnnotations;

namespace xilopro2.Data.Entities
{
    public class Lineup
    {
        [Key]
        public int Lineup_ID { get; set; }

        [Display(Name = "Es Titular?")]
        public bool Lineup_IsTitular { get; set; }

        public int EntraPor { get; set; }

        public int Salepor { get; set; }

        public int MinEntra { get; set; }

        public int MinSale { get; set; }

        public int MinJugados => Lineup_IsTitular ? MinSale - 0 : MinSale - MinEntra;

        public Player? Player { get; set; }
    }
}
