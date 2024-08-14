using L07.Data;
using L07.Model;
using Microsoft.EntityFrameworkCore;

namespace L07.Data
{
    public class DBWebAPIRepo : IWebAPIRepo
    {
        private readonly WebAPIDBContext _dbContext;

        // Adjusted constructor
        public DBWebAPIRepo(WebAPIDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            
            IEnumerable<Customer> customer = _dbContext.Customer.ToList();
            return customer;
        }

        public Customer GetCustomerByID(int id)
        {
            Customer customer = _dbContext.Customer.FirstOrDefault(c => c.Id == id);
            return customer;
        }

        public Customer AddCustomer(Customer customer)
        {
            _dbContext.Customer.Add(customer);
            _dbContext.SaveChanges();
            return customer;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
