using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectsToTuples
{
    public class Program
    {
        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            Console.WriteLine("Hello!");
        }

        public static double DistanceBetween((double X, double Y) p1, (double X, double Y) p2)
        {
            double xDistance = p2.X - p1.X;
            double yDistance = p2.Y - p1.Y;
            double distance = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));
            return distance;
        }
    }

    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void ExampleTest()
        {
            (double, double) p1 = (0, 0);
            (double, double) p2 = (3, 4);
            double distance = Program.DistanceBetween(p1, p2);
            Assert.AreEqual(5, distance, 0.1);
        }
    }
}