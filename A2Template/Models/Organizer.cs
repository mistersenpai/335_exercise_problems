using System.ComponentModel.DataAnnotations;

namespace A2.Models
{
    public class Organizer
    {
        [Key]
        [Required]
        public int Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}