using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.ViewModel
{
    public class CityVM
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="City Name Is Required.")]
        [StringLength(50)]
        public string Name { get; set; }
        public int StateId { get; set; }
    }
}
