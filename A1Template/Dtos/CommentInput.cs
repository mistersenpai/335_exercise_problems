﻿using System.ComponentModel.DataAnnotations;

namespace A1.Dtos
{
    public class CommentInput
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserComment { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string IP { get; set; }
    }
}