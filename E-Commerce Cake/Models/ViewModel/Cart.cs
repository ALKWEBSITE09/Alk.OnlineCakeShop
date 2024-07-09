using E_Commerce_Cake.Models.Database;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.ViewModel
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int itemId { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public double Bill {  get; set; }
    }
}
