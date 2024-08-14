using System.ComponentModel.DataAnnotations;

namespace L07.Dtos
{
    public class CustomerInputDto
    {
        [Required]
         public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
