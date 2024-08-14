using L07.Model;

namespace L07.Data
{
    public interface IWebAPIRepo
    {
        IEnumerable<Customer> GetAllCustomers();

        Customer GetCustomerByID(int id);

        Customer AddCustomer(Customer customer);
    }
}
