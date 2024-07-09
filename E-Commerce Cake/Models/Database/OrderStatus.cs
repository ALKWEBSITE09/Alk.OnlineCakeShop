using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.Database
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
