using System.ComponentModel.DataAnnotations;

namespace xilopro2.Data.Entities
{
    public class ListaIdsGroup
    {
        [Key]
        public int Id { get; set; }
        public List<int> Valores { get; set; }
    }
}
