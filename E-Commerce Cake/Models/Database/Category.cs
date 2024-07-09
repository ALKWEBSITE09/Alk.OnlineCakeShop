using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Cake.Models.Database
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tittle { get; set; }
    }
}
