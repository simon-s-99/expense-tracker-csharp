using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uppg_6._1o2o3;

/*
 * Felet uppstår av att vi matar in -1 som largest, om programmet får
 * input som är "lägre" än -1 (t.ex. -50, -70, -900) blir largest
 * fortfarande -1
 * 
 * Felet går att åtgärda genom att ändra typen av largest från int till
 * int? (nullable int) och ge largest null som startvärde samt ändra 
 * så att vi faktiskt tar hänsyn till "större" negativa värden
 * (t.ex. att -1090 är "större" än -475) 
 */

namespace NullableValueTypes
{
    public class Program
    {
        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            Console.WriteLine("Enter one number on each line, and a blank line when you are done:");
            List<int> numbers = new List<int>();
            bool done = false;
            while (!done)
            {
                string s = Console.ReadLine();
                if (s == "")
                {
                    done = true;
                }
                else
                {
                    int n = int.Parse(s);
                    numbers.Add(n);
                }
            }

            int? largest = null;
            foreach (int n in numbers)
            {
                if (largest == null)
                {
                    largest = n;
                }
                else if (n < 0)
                {
                     if ((n * -1) > (largest * -1)) 
                     { 
                         largest = n; 
                     }
                }
                else if (n > largest)
                {
                    largest = n;
                }
            }
            Console.WriteLine("The largest number is: " + largest);
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