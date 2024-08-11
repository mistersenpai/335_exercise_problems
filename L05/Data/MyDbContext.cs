using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using L05.Model;

namespace L05.Data
{
    class MyDbContext : DbContext
    {
        // Setting up SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MyDatabase.sqlite");
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; } //products is table in db, product is obj

    }
}
