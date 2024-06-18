using Microsoft.AspNetCore.Mvc.Rendering;

namespace xilopro2.Models
{
    public class GroupedMatchViewModel
    {
        public string GroupName { get; set; }
        public List<SelectListItem> Matches { get; set; }
        public int JornadaId { get; set; }
        public bool IsChecked { get; set; }
    }
}
