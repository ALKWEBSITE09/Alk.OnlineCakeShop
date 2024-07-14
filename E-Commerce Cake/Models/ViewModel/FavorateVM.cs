using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.ViewModel
{
    public class FavorateVM
    {
        [Key]
        public int Id { get; set; }
        public int itemId { get; set; }
        public int UsersId { get; set; }

    }
}
