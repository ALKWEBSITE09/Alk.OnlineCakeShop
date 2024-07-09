using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.Database
{
    public class ChoicePrice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Prices { get; set; }
    }
}
