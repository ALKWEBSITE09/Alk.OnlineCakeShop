using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Cake.Models.Database
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Required]
        public int CategoryesId { get; set; }
        [ForeignKey(nameof(CategoryesId))]
        public Category Cg { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory Scg { get; set; }
        public string ImagePath { get; set; }

    }
}
