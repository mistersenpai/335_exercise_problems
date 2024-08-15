using ex07.Model;
using Microsoft.EntityFrameworkCore;

namespace ex07.Data
{
    public class WebAPIDBContext: DbContext
    {
        public WebAPIDBContext(DbContextOptions<WebAPIDBContext> options) : base(options) { }

        public DbSet<Phone> Phones { get; set; }
    }
}
