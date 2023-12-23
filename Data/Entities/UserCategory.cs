using System.ComponentModel.DataAnnotations;

namespace xilopro2.Data.Entities
{
    public class UserCategory
    {
        //  [Key]
       // public int Id { get; set; }
        public int Category_ID { get; set; }
        public string User_ID { get; set; }

        public Category Category { get; set; }
        public AppUser User { get; set; }


    }
}
