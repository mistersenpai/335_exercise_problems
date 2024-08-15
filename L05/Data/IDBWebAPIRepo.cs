using L05.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05.Data
{
    public interface IDBWebAPIRepo
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerByID(int id);
        Customer AddCustomer(Customer customer);

    }
}
