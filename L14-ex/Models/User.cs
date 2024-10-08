﻿using System.ComponentModel.DataAnnotations;

namespace L14_ex.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Position { get; set; }
    }
}
