using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.Database
{
    public class PriceCheck
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
