using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Cake.Models.Database
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, 5, ErrorMessage = "Enter 0 To 5 Number")]
        public int Rating { get; set; }
        public int ItemId { get; set; }
        [ForeignKey(nameof(ItemId))]
        public Product Item { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
