using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using xilopro2.Data.Entities;

namespace xilopro2.Models
{
    public class MatchViewModel
    {

        public int MatchID { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime DateLocal => Date.ToLocalTime();

        [Display(Name = "Jornada:")]
        public string Jornada { get; set; }

        [Display(Name = "Goles Local")]
        public int? GoalsLocal { get; set; }

        [Display(Name = "Goles Visitante")]
        public int? GoalsVisitor { get; set; }

        [Display(Name = "Is Closed?")]
        public bool IsClosed { get; set; }





        public int GroupId { get; set; }
        public string GroupName { get; set; }

    
        [Display(Name = "Local")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un equipo.")]

        public int LocalId { get; set; }


        [Display(Name = "Visitante")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un equipo.")]

        public int VisitorId { get; set; }

        public List<Team>? Teams { get; set; }

   

    }

}
