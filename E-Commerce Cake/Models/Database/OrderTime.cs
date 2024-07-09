using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.Database
{
    public class OrderTime
    {
        [Key]
        public int Id { get; set; }
        public DateTime Name { get; set; }
    }
}
