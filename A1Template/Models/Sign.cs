using System.ComponentModel.DataAnnotations;

namespace A1.Model
{
    public class Sign
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}