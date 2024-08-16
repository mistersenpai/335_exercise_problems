using System.ComponentModel.DataAnnotations;

namespace A1.Model
{
    public class Sign
    {
        [Key]
        public string Id { get; set; }
        public string Description { get; set; }
    }
}