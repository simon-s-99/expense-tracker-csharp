using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EnumExample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uppg_2._1;

/*
 * EnumExample
 * 
 * Fördelarna med enums i ett sånt här scenario är att vi inte 
 * kan mata in fel alternativ när vi programmerar. Vi får då error
 * när vi skrivit fel / stavat fel. 
 * Det förhindrar alltså små slarvfel i koden om vi jämför detta mot
 * att använda strings istället. 
 */

namespace EnumExample
{
    public enum AgeCategory { Child, Senior, Adult }
    public class Person
    {
        public string FirstName;
        public string LastName;
        public AgeCategory AgeCategory;
    }

    public class Program
    {
        public static void Main()
        {
            Person p = new Person
            {
                FirstName = "Brad",
                LastName = "Pitt",
                AgeCategory = AgeCategory.Adult
            };

            if (p.AgeCategory == AgeCategory.Senior)
            {
                Console.WriteLine("You will receive a pension.");
            }
            else if (p.AgeCategory == AgeCategory.Child) 
            {
                Console.WriteLine("You will receive child benefit.");
            }
            else if (p.AgeCategory == AgeCategory.Adult)
            {
                Console.WriteLine("You will receive no extra money. :(");
            }
            else
            {
                Console.WriteLine("Invalid category.");
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