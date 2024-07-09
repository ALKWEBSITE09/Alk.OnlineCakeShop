using E_Commerce_Cake.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.ViewModel
{
    public class InvoiceVM
    {
        [Key]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string CouponCode { get; set; }
        public int UsertId { get; set; }
        public string UserEmail { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string AreaName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double TotalBill { get; set; }
    }
}
