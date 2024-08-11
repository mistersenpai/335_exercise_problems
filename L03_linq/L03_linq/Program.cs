using System;
using System.Collections.Generic;
using System.Linq;

namespace L03_linq
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    List<Person> people = new List<Person>{
        //    new Person { Id = 1, Name = "Bob", Age = 19 },
        //    new Person { Id = 2, Name = "Tom", Age = 32 },
        //    new Person { Id = 3, Name = "Joe", Age = 28 },
        //    };
            

        //    Person peopleover30 = people.FirstOrDefault(people => people.Age > 30);

        //    if (peopleover30 == null) {
        //        Console.WriteLine("No Person found over the age of 30");
        //    } else
        //    {
        //        Console.WriteLine($"First person over 30: {peopleover30.Name}, {peopleover30.Age}");
        //    }
            



        //}


        static void Main1(string[] args)
        {
            int[] numbers = { 1, 2, 12, 45, 67, 74, 88, 92, 108, 132, 145, 160 };

            int firstNumbergreater20 = numbers.FirstOrDefault(x =>  (x > 20) && (x % 2 == 0) );

            if (firstNumbergreater20 == null)
            {
                Console.WriteLine("No even number found");
            }
            else
            {
                Console.WriteLine($"First even number found greater than 20: {firstNumbergreater20}");
            }


        }
    }
}