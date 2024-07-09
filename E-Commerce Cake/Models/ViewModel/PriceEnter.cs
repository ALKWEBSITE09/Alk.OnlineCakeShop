using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.ViewModel
{
    public class PriceEnter
    {
        [Key]
        public int Id { get; set; }
        public double priceId { get; set; }
    }
}
