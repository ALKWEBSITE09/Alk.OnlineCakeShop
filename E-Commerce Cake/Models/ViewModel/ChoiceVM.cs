using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.ViewModel
{
    public class ChoiceVM
    {
        [Key]
        public int Id { get; set; }
        public IFormFile Image { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int subId { get; set; }

        public double PriceId { get; set; }
        public int PriceCheckId { get; set; }
        public int ChoiceorderId { get; set; }
        public int UserId { get; set; }
    }
}
