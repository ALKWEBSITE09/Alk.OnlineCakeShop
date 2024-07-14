using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Cake.Models.Database
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public string TransId { get; set; }
        public string OrderId { get; set; }
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public OrderStatus Status { get; set; }
        public int InvId { get; set; }
        [ForeignKey(nameof(InvId))]
        public Invoice Inv { get; set; }

    }
}
