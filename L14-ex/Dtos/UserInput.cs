using System.ComponentModel.DataAnnotations;

namespace L14_ex.Dtos
{
    public class UserInput
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Position { get; set; }
    }
}
