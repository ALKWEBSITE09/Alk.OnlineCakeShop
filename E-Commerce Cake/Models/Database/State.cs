using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.Database
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="State Name Is Required.")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
