﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Cake.Models.Database
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [DataType(DataType.Date)]
        public string ShopOpenDate { get; set; }
        [Required]
        [StringLength(100)]
        public string ShopName { get; set; }
        [StringLength(100)]
        public string ShopOwnerName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15, MinimumLength = 10)]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Pass { get; set; }
        [Required]
        [Compare("Pass")]
        [DataType(DataType.Password)]
        public string ConfirmPass { get; set; }
        public int UserTypesId { get; set; }
        [ForeignKey(nameof(UserTypesId))]
        public UserType customer { get; set; }
    }
}
