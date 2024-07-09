using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Cake.Models.Database
{
    public class Favorate
    {
        [Key]
        public int Id { get; set; }
        public int itemId { get; set; }
        [ForeignKey(nameof(itemId))]
        public Product Item { get; set; }
        public int UsersId { get; set; }
        [ForeignKey(nameof(UsersId))]
        public User Customer { get; set; }
    }
}
