using Microsoft.EntityFrameworkCore;
using L14_ex.Models;

namespace L14_ex.Data
{
    public class L14DbContext: DbContext
    {
        public L14DbContext(DbContextOptions<L14DbContext> options) : base(options) { }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
