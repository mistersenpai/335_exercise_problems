using System.ComponentModel;

namespace L07.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
    }
}

    // add package Microsoft.EntityFrameworkCore.Design^C