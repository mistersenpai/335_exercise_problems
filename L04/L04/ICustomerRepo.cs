using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L04
{
    internal interface ICustomerRepo
    {
        IEnumerable<Person> GetAllCustomer();
        IEnumerable<Person> returnAllPeopleWithName(string Name);

    }
}
