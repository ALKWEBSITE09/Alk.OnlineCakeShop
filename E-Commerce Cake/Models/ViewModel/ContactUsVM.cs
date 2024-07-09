using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.ViewModel
{
    public class ContactUsVM
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Email Is Required.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Enter Valid Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Name Is Required.")]
        [StringLength(20)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description Is Required.")]
        [StringLength(500)]
        public string Description { get; set; }
    }
}
