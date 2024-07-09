using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Cake.Models.Database
{
    public class Choice
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public double PriceId { get; set; }
        public int subId { get; set; }
        [ForeignKey(nameof(subId))]
        public SubCategory subcate { get; set; }
        public int PriceCheckId { get; set; }
        [ForeignKey(nameof(PriceCheckId))]
        public PriceCheck Pc { get; set; }
        public int ChoiceorderId { get; set;}
        [ForeignKey(nameof(ChoiceorderId))]

        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User MyProperty { get; set; }
    }
}
