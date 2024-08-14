using L07.Model;
using Microsoft.EntityFrameworkCore;

namespace L07.Data
{
    public class WebAPIDBContext: DbContext
    {
        public WebAPIDBContext(DbContextOptions<WebAPIDBContext> options) : base(options) { }
        public DbSet<Customer> Customer { get; set; }


    }
}
