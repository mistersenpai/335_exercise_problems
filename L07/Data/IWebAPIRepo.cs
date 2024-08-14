using L07.Model;

namespace L07.Data
{
    public interface IWebAPIRepo
    {
        IEnumerable<Customer> GetAllCustomers();

        Customer getCustomerByID(int id);

        Customer AddCustomer(Customer customer);
    }
}
