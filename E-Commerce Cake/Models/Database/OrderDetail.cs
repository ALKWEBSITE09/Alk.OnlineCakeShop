using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Cake.Models.Database
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int itemId { get; set; }
        [ForeignKey(nameof(itemId))]
        public Product Item { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public double SubTotal { get; set; }
        public int invId { get; set; }
        [ForeignKey(nameof(invId))]
        public Invoice inv { get; set; }

    }
}
