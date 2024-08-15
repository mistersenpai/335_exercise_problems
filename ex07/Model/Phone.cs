using System.ComponentModel.DataAnnotations;

namespace ex07.Model
{
    public class Phone
    {
        [Key]
        public string PhoneID { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public float Price { get; set; }

        

    }
}
