using L07.Model;
using Microsoft.EntityFrameworkCore;

namespace L07.Data
{
    public class WebAPIDBContext: DbContext
    {
        public WebAPIDBContext(DbContextOptions<WebAPIDBContext> options) : base(options) { }
        public DbSet<Customer> Customer { get; set; }

        // Override OnConfiguring method to configure database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure database connection
            optionsBuilder.UseSqlite("Data Source=ex1.sqlite");
        }
    }
}
