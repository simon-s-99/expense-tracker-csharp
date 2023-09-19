using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace uppg_4._1o2
{
    public class Program
    {
        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            // uppg. 4.1
            string city = "Gothenburg";
            int year = 1621;
            Console.WriteLine($"{city} was founded in {year}");

            // uppg 4.2 
            double bmi1 = 20;
            double bmi2 = 21.56789;
            // The program should print "20.0" and "21.6" below.
            Console.WriteLine($"{bmi1.ToString("0.0")}");
            Console.WriteLine($"{bmi2.ToString("0.0")}");
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
