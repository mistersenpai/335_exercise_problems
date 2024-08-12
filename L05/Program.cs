using L05.Data;
using L05.Model;
using System;
using System.Security.AccessControl;

namespace L05;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        //AddRecords();
        //GetRecords();
        //DeleteRecords();
        //TrashyUpdateRecord();
        UpdateRecord();
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
        using (MyDbContext dbContext = new MyDbContext()) {

            IEnumerable<Customer> customerlist = dbContext.Customers.Where(p => p.Id > 0);

            if (customerlist != null)
            {
                foreach (Customer c in customerlist)
                {
                    Console.WriteLine($"Id:{c.Id}, Name: {c.FirstName}");
                }
            }
            else {
                Console.WriteLine("DB is empty");
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

    private static void TrashyUpdateRecord()
    {
        using (var dbContext = new MyDbContext())
        {
            int toUpdate = 4;
            string newFirstName = "hello";
            string newLastName = "pops";
            string newEmail = "hellopops@gmail.net";


            
            Customer c = dbContext.Customers.FirstOrDefault(p => p.Id == toUpdate);

            if (c != null)
            {
                // Delete instant of object 
                Console.WriteLine($"Deleting {c.Id}...");
                dbContext.Remove(c);
                dbContext.SaveChanges();
                Console.WriteLine("Succesfully deleted");


                // Re Adding instance of object with new values
                Customer newCustomer = new Customer { Id = toUpdate, FirstName = newFirstName, LastName = newLastName, Email = newEmail };
                Console.WriteLine($"Readding {newCustomer.Id}");
                dbContext.Customers.Add(newCustomer);
                dbContext.SaveChanges();
                Console.WriteLine("Sucessfully readded");
            
            }
            else
            {
                Console.WriteLine("Does not exist in DB");
            }


        }
    }

    private static void UpdateRecord()
    {
        using (var dbContext = new MyDbContext())
        { 
            //legit way of updating

            Customer tobeUpdated = dbContext.Customers.FirstOrDefault(c => c.Id == 4);

            if (tobeUpdated != null) 
            {
                //run update
                tobeUpdated.FirstName = "Bobby";
                dbContext.SaveChanges();
                Console.WriteLine("Sucessfully Updated");
            }
            else
            {
                Console.WriteLine("Does not exist");
            }
        }
    }
}
