using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace uppg_7_HideString
{
    public class Program
    {
        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            string methodOutput = HideString("hidethis");
            Console.WriteLine(methodOutput);
            methodOutput = HideString("hidden differently", 'X');
            Console.WriteLine(methodOutput);
        }

        public static string HideString(string strToHide, char replaceStrWith = '*')
        {
            string hiddenString = string.Empty;

            foreach (char c in strToHide)
            {
                hiddenString += replaceStrWith;
            }

            return hiddenString;
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
