using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Cake.Models.Database
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public int TimeId { get; set; }
        public OrderTime Time { get; set; }

        public string CouponCode { get; set; }
        public int UsertId { get; set; }
        [ForeignKey(nameof(UsertId))]
        public User Customer { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public City Citys { get; set; }
        public int StateId { get; set; }
        [ForeignKey(nameof(StateId))]
        public State States { get; set; }
        public int AreaId { get; set; }
        [ForeignKey(nameof(AreaId))]
        public Area Areas { get; set; }
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15, MinimumLength = 10)]
        public string Phone { get; set; }
        public double TotalBill { get; set; }
        public int OrderStatusId { get; set; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus Status { get; set; }
    }
}
