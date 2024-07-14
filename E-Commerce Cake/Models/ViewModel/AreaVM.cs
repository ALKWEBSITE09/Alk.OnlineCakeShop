using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.ViewModel
{
    public class AreaVM
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Area Name Is Required.")]
        [StringLength(50)]
        public string Name { get; set; }
        public int CityId { get; set; }
    }
}
