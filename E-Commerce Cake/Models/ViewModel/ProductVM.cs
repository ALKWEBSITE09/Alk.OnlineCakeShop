using E_Commerce_Cake.Models.Database;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.ViewModel
{
    public class ProductVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int CategoryesId { get; set; }
        public int SubCategoryesId { get; set; }
        [NotMapped]
        public IFormFile ImagePath { get; set; }
    }
}
