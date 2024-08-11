using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace L05.Model
{
    class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Category { get; set; }
        public string? Brand { get; set; }

    }
}
