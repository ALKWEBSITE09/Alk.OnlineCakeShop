using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.ViewModel
{
    public class SubCategoryVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string tittle { get; set; }
        [Required]
        public int CategoryesId { get; set; }
    }
}
