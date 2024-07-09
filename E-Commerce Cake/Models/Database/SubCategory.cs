using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Cake.Models.Database
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string tittle { get; set; }
        [Required]
        public int CategoryesId { get; set; }
        [ForeignKey(nameof(CategoryesId))]
        public Category Cg { get; set; }

    }
}
