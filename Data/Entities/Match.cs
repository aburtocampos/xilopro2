using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace xilopro2.Data.Entities
{
    public class Match
    {
        [Key]
        public int Match_ID { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        [NotMapped]
        public Team Local { get; set; }

        [NotMapped]
        public Team Visitor { get; set; }

        [Display(Name = "Goals Local")]
        public int GoalsLocal { get; set; }

        [Display(Name = "Goals Visitor")]
        public int GoalsVisitor { get; set; }

        [Display(Name = "Is Closed?")]
        public bool IsClosed { get; set; }


        [Display(Name = "Jugador")]
        public Player Player { get; set; }

        [Display(Name = "Tecnico")]
        public AppUser User { get; set; }

        //relations

        public Groups Groups { get; set; }

    }
}
