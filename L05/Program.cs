using L05.Data;
using L05.Model;
using System;

namespace L05;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        //AddRecords();
        GetRecords();
        //DeleteRecords();
    }


    private static void AddRecords()
    {
        // Create data
        List<Customer> newCustomers = new List<Customer>()
        {
            //new Customer{Id=1, FirstName="Matt", LastName="mate", Email="mattmate@gmail.com"},
            //new Customer{Id=2, FirstName="Ego", LastName="Frodo", Email="egofrodo@gmail.com"},
            new Customer{Id=4, FirstName="joema", LastName="Tyre"},
        };

        // Connect to db
        using(MyDbContext dbContext = new MyDbContext())
        {
            // Add all objects
            foreach (Customer c in newCustomers) {
                dbContext.Customers.Add(c);

            // Push changes to DB to save
            dbContext.SaveChanges();
            }

        }
    }

    private static void GetRecords()
    {
        using (MyDbContext dbContext = new MyDbContext())
        {
            Console.WriteLine("In here");
            IEnumerable<Customer> customers = dbContext.Customers.Where(c => c.Id < 4);

            Console.WriteLine("The data", customers);

            if (customers != null)
            {
                foreach (Customer c in customers)
                {
                    Console.WriteLine($"Customer: {c.FirstName}, ID: {c.Id}, Email: {c.Email}");
                }
            }
            else if (customers != null) 
            {
                Console.WriteLine("customers is null");
            }

            

            
        }
    }

    private static void DeleteRecords() 
    {
        using (MyDbContext dbContext = new MyDbContext()) 
        {
            Customer c = dbContext.Customers.FirstOrDefault(c => c.Id == 4);

            if (c != null) 
            { 
                Console.WriteLine($"Customer {c.Id} deleting...");
                dbContext.Remove(c);
                dbContext.SaveChanges();
                Console.WriteLine("Succesfully Deleted");
            }
            else 
            {
                Console.WriteLine("No such file");
            }

        }
    }
}
