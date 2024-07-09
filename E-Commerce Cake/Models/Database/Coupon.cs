using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.Database
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int NoofCoupon { get; set; }
        public string CouponCode { get; set; }
        public CouponType Type { get; set; }
        public double Discount { get; set; }
        public double MinimumAmount { get; set; }
        public string CouponImage { get; set; }
        public bool IsActive { get; set; }


    }
    public enum CouponType
    {
        Percent = 0,
        Currency = 1,
    }
}
