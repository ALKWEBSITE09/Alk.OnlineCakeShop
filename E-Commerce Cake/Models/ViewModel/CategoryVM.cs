using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.ViewModel
{
    public class CategoryVM
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Category Name field is required.")]
        public string Tittle { get; set; }
    }
}
