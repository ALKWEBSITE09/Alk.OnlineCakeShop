using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Cake.Models.Database
{
    public class User
    {
        [Key]
        public int? Id { get; set; }
        public string? Image { get; set; }

        [Required]
        [StringLength(100)]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string? LastName { get; set; }
        [DataType(DataType.Date)]
        public string? BirthDate { get; set; }
        public string? Username { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15,MinimumLength = 10)]
        public string? Phone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be 8-15 characters long, contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public string? Pass { get; set; }
        [Required]
        [Compare("Pass")]
        public string? ConfirmPass { get; set; }
        public int UserTypesId { get; set; }
        [ForeignKey(nameof(UserTypesId))]
        public UserType customer { get; set; }
    }
}
