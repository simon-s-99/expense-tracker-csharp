using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;

namespace ExpenseTracker
{
    public class Expense
    {
        // Add variables here.
        public string Name;
        public string Category;
        public decimal Price;
        public decimal VAT;
        public decimal PriceWithoutVAT;
    }

    public class Program
    {
        public static List<Expense> Expenses = new List<Expense>();
        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            // Write the main program code here.
            
            // pris inkl moms
            //pris exl moms
            // kategori + momsen på den/det
            // namn


            int menu = ShowMenu("Vad vill du göra?", new[]
            {
                "Lägg till utgift",
                "Visa alla utgifter",
                "Visa summa per katerogi",
                "Ändra utgift",
                "Ta bort enskild utgift",
                "Ta bort samtliga utgifter",
                "Avsluta"
            });
            Console.Clear();
            if (menu == 0)
            {
                Console.Write("Namn: ");
                string name = Console.ReadLine();
                Console.Write("Pris: ");
                double price = double.Parse(Console.ReadLine());

                int category = ShowMenu("Kategori", new[]
                {
                    "Utbildning",
                    "Böcker,",
                    "Livsmedel",
                    "Övrigt"
                });
                Console.Clear();
                if (category == 0)
                {

                }

               
            }
            if (menu == 1)
            {
                // Visa alla utgifter
            }
            if (menu == 2)
            {
                //Visa summa per kategori
            }
            if (menu == 3)
            {
                // Ändra på någon utgift
            }
            if (menu == 4)
            {
                // Ta bort utgift
            }
            if (menu == 5)
            {
                //Ta bort alla utgifter
            }
            if (menu == 6)
            {
                Console.Write("Exiting program. Goodbye!");
            }
        }
        public static void ListExpenses()
        {
            if (Expenses.Count == 0)
            {
                Console.WriteLine("Du har inte lagt till några utgifter ännu");
            }
            else
            {
                //Ändra dessa till redan tillagda utgifter.
                foreach (Expense Expense in Expenses)
                {
                    Console.WriteLine("- " + FullName(contact) +
                        ", " + contact.Email + ", " + contact.PhoneNumber
                    );)
                }
            }
        }


        // Return the sum of all expenses in the specified list, with or without VAT based on the second parameter.
        // This method *must* be in the program and *must* be used in both the main program and in the tests.
        public static decimal SumExpenses(List<Expense> expenses, bool includeVAT)
        {
            decimal sum = 0;
            // Implement the rest of this method here.
            return sum;
        }

        // method to get VAT associated with category
        public static double GetVAT(string category)
        {
            double rValue = 0.0;
            switch (category)
            {
                case "Utbildning":
                    // Change nothing since rValue is already 0 
                    break;

                case "Böcker":
                    rValue = 0.06; // books = 6% VAT
                    break;

                case "Livsmedel":
                    rValue = 0.12; // food = 12% VAT
                    break;

                case "Övrigt":
                    rValue = 0.25; // other = 25% VAT
                    break;

                default:
                    // Handles incorrect input, mainly for testing 
                    break;
            }
            return rValue; 
        }

        // Do not change this method.
        // For more information about ShowMenu: https://startcoding.app/console/
        public static int ShowMenu(string prompt, IEnumerable<string> options)
        {
            if (options == null || options.Count() == 0)
            {
                throw new ArgumentException("Cannot show a menu for an empty list of options.");
            }

            Console.WriteLine(prompt);

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            // Calculate the width of the widest option so we can make them all the same width later.
            int width = options.Max(option => option.Length);

            int selected = 0;
            int top = Console.CursorTop;
            for (int i = 0; i < options.Count(); i++)
            {
                // Start by highlighting the first option.
                if (i == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                var option = options.ElementAt(i);
                // Pad every option to make them the same width, so the highlight is equally wide everywhere.
                Console.WriteLine("- " + option.PadRight(width));

                Console.ResetColor();
            }
            Console.CursorLeft = 0;
            Console.CursorTop = top - 1;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(intercept: true).Key;

                // First restore the previously selected option so it's not highlighted anymore.
                Console.CursorTop = top + selected;
                string oldOption = options.ElementAt(selected);
                Console.Write("- " + oldOption.PadRight(width));
                Console.CursorLeft = 0;
                Console.ResetColor();

                // Then find the new selected option.
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Count() - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }

                // Finally highlight the new selected option.
                Console.CursorTop = top + selected;
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                string newOption = options.ElementAt(selected);
                Console.Write("- " + newOption.PadRight(width));
                Console.CursorLeft = 0;
                // Place the cursor one step above the new selected option so that we can scroll and also see the option above.
                Console.CursorTop = top + selected - 1;
                Console.ResetColor();
            }

            // Afterwards, place the cursor below the menu so we can see whatever comes next.
            Console.CursorTop = top + options.Count();

            // Show the cursor again and return the selected option.
            Console.CursorVisible = true;
            return selected;
        }
    }

    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void SumExpensesTest1()
        {
            // Write code here to test the SumExpenses method.
        }

        [TestMethod]
        public void SumExpensesTest2()
        {
            // Write code here to test the SumExpenses method.
        }

        [TestMethod]
        public void SumExpensesTest3()
        {
            // Write code here to test the SumExpenses method.
        }

        [TestMethod]
        public void GetVATEducation()
        {
            string testInput = "Utbildning";
            double expected = 0.0;
            double result = Program.GetVAT(testInput);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetVATBooks()
        {
            string testInput = "Böcker";
            double expected = 0.06;
            double result = Program.GetVAT(testInput);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetVATFood()
        {
            string testInput = "Livsmedel";
            double expected = 0.12;
            double result = Program.GetVAT(testInput);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetVATOther()
        {
            string testInput = "Övrigt";
            double expected = 0.25;
            double result = Program.GetVAT(testInput);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void GetVATEmptyParameter()
        {
            string testInput = "";
            double expected = 0.0;
            double result = Program.GetVAT(testInput);
            Assert.AreEqual(expected, result);
        }
    }
}