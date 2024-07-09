using E_Commerce_Cake.Models.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Cake.Models.ViewModel
{
    public class CouponVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tittle { get; set; }
        [Required]

        public int NoofCoupon { get; set; }
        public string CouponCode { get; set; }
        [Required]
        public CouponType Type { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public double MinimumAmount { get; set; }
        [NotMapped]
        public IFormFile CouponImage { get; set; }

        public bool IsActive { get; set; }
    }
}
