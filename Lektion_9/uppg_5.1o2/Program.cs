using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace uppg_5._1o2
{
    public class Program
    {
        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            // uppg. 5.1 
            //double temperature = 50;
            //string message = temperature >= 100 ? message = "Boiling" : message = "Normal";

            // uppg. 5.2 
            double temperature = 100;
            string message = temperature >= 100 ? "Boiling" : (temperature <= 0 ? "Freezing" : "Normal");
            Console.WriteLine(message);
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
