using System.Net.Mail;

namespace L04
{
    class Program
    {
        static void ex1(string[] args)
        {
            List<Person> people = new List<Person>() {
            new Person{Id=1,Name="Bob",Age=43},
            new Person{Id=2,Name="Charlie",Age=29},
            new Person{Id=3,Name="Sussy",Age=30},
            new Person{Id=4,Name="Joe",Age=32},
            };

            IEnumerable<Person> peopleOver30 = people.Where(people => people.Age > 30);
            Console.WriteLine("Person older than 30:");
            foreach (Person person in peopleOver30)
            {
                Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");

            }
        }

        static void ex2(string[] args)
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee {Id=0,Name="Joe", Department="Sales", Salary=45000.0},
                new Employee {Id=1,Name="Susan", Department="IB", Salary=78000.0},
                new Employee {Id=2,Name="Dom", Department="Sales", Salary=55000.0},
                new Employee {Id=3,Name="Jack", Department="Cleaner", Salary=32000.0},
                new Employee {Id=4,Name="Jason", Department="Sales", Salary=73000.0},
            };

            IEnumerable<Employee> sortedEmployee = employees.Where(employee => (employee.Department == "Sales" && employee.Salary > 50000.0));

            Console.WriteLine("Employees from the sales department with a salary greater than $50,000");
            foreach (Employee employee in sortedEmployee)
            {
                Console.WriteLine($"Name: {employee.Name}, Department: {employee.Department}, Salary: ${employee.Salary}");
            }
        }

        static void ex3(string[] args)
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee {Id=0,Name="Joe", Department="Sales", Salary=45000.0},
                new Employee {Id=1,Name="Susan", Department="IB", Salary=78000.0},
                new Employee {Id=2,Name="Dom", Department="Sales", Salary=55000.0},
                new Employee {Id=3,Name="Jack", Department="Cleaner", Salary=32000.0},
                new Employee {Id=4,Name="Jason", Department="Sales", Salary=73000.0},
            };

            var sortedlist = employees.Select(e => new { Name = e.Name, Role = e.Department });

            //var x = sortedlist.Append(new { Name='Jessica',Role='Sales'});


            Console.WriteLine("Mapping of names to roles:");
            foreach (var employee in sortedlist)
            {
                Console.WriteLine($"Name: {employee.Name}, Role: {employee.Role}");
            }
        }

        static void ex4(string[] args)
        {
            //List<Person> persons = new List<Person>() {
            //    new Person {Id= 0,Name="Bob", Age= 45},
            //    new Person {Id= 1,Name="Vladimir", Age= 32},
            //    new Person {Id= 2,Name="Rio", Age= 19},
            //    new Person {Id= 3,Name="Hannah", Age= 20},
            //    new Person {Id= 4,Name="Paramvir", Age= 53 },
            //};

            List<Person> persons = new List<Person>() {
                new Person {Id= 0,Name="Rio", Age= 19},
                new Person {Id= 1,Name="Charlie", Age= 30},
                new Person {Id= 2,Name="David", Age= 20},
                new Person {Id= 3,Name="Alice", Age= 25},
                new Person {Id= 4,Name="Eve", Age= 30 },
            };

            IEnumerable<Person> sortedPeople = persons.OrderBy(e => e.Age).ThenBy(e => e.Name);

            Console.WriteLine("Sorted list of persons");
            foreach (Person person in sortedPeople) {
                Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
            }

        }

        static void test(string[] args)
            {
                MockCustomerRepo repo = new MockCustomerRepo();

                IEnumerable<Person> names = repo.returnAllPeopleWithName("eee");

                IEnumerable<Person> allppl = repo.GetAllCustomer();
                IEnumerable<int> people = allppl.Select(e => e.Age);

                foreach (int age in people) { Console.WriteLine(age); }

                Console.WriteLine("List of people:");
                foreach (Person name in names)
                {
                    Console.WriteLine($"person name is {name.Name} and id is {name.Id}");
                }

                Console.WriteLine("----------");

                int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

                IEnumerable<int> GreaterThan5AndEven = numbers.Where(x => (x > 5 && x % 2 == 0));

                Console.WriteLine($"List of numbers is:");
                foreach (int num in GreaterThan5AndEven)
                {
                    Console.WriteLine(num);
                }


            }


    }
}

