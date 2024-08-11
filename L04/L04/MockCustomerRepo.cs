using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L04
{
    internal class MockCustomerRepo: ICustomerRepo
    {
        List<Person> _persons;
        public MockCustomerRepo() {
            this._persons = new List<Person>() { 
            new Person{Id=1,Name="hello",Age=2},
            new Person{Id=2,Name="eee",Age=22},
            new Person{Id=3,Name="eee",Age=33}
            };
        }

        public IEnumerable<Person> GetAllCustomer() {  return this._persons; }

        public IEnumerable<Person> returnAllPeopleWithName(string Name)
        {
            IEnumerable<Person> p = _persons.Where(e => e.Name == Name);

            return p;
        }
    }
}
