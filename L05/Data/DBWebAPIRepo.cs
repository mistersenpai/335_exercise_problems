using L05.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05.Data
{
    class DBWebAPIRepo: IDBWebAPIRepo
    {
        public DBWebAPIRepo(WebAPIDBContext dbContext) { 
            
        
        }

        public  IEnumerable<Customer> GetAllCustomers() { }


        public Customer GetCustomerByID(int id) { }

        public void AddCustomer(Customer customer) { }


    }
}
