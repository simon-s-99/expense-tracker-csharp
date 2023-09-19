using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Lektion_9;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// lektion 9 uppg. 1 
// CodeStyle 

namespace CodeStyle
{
    public class Person // persons => Person 
    {
        public string FirstName; // first_name => FirstName
        public string LastName; // lastName => LastName
        public int Age; // AGE => Age
    }

    public class Program
    {
        public static void Main()
        {
            Console.Write("First name: ");
            string firstName = Console.ReadLine(); // strFirstName => firstName 

            Console.Write("Last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine()); // x => age 

            Person person = new()    // new Person => new()  |  p => person 
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age
            };

            if (person.Age == 20)
            {
                Console.WriteLine("You are 20");
            }
            else  // else if (person.Age != 20) => else 
            {
                Console.WriteLine("You are not 20");
            }
        }
    }

    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void ExampleTest()
        {
            using FakeConsole console = new FakeConsole("First input", "Second input");
            Program.Main();
            Assert.AreEqual("Hello!", console.Output);
        }
    }
}