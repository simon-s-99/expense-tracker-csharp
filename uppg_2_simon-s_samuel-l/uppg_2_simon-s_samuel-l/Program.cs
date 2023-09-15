using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;

// Assignment nr. 2 
// by Simon Sörqvist & Samuel Lööf 

/*
 * Check before turning in: 
 * 
 * Removed unnecessary comments? i.e. Jakobs instruktioner?
 * Are there any rounding errors ? 
 */

namespace ExpenseTracker
{
    public class Expense
    {
        // Add variables here.
        public string Name;
        public string Category;
        public decimal Price;
        public decimal VAT;
        public decimal PriceWithoutVAT; // is this needed ? 
                                    // made redundant by GetVAT() ? 
    }

    public class Program
    {
        // Static List to hold all expenses throughout the program
        public static List<Expense> Expenses = new List<Expense>();
        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            // Write the main program code here.

            // pris inkl moms
            // pris exl moms
            // kategori + momsen på den/det
            // namn

            // main-loop, ends if user chooses option 6 in mainMenu
            while (true) 
            { 
                int mainMenu = ShowMenu("Vad vill du göra?", new[]
                {
                    "Lägg till utgift",
                    "Visa alla utgifter",
                    "Visa summa per kategori",
                    "Ändra utgift",
                    "Ta bort enskild utgift",
                    "Ta bort samtliga utgifter",
                    "Avsluta"
                });
                Console.Clear();

                if (mainMenu == 0)  // 0 = add expense 
                {
                    AddExpense();
                }
                else if (mainMenu == 1) // list all expenses
                {
                    ListExpenses();
                }
                else if (mainMenu == 2) // show sum per category 
                {
                    // code here
                }
                else if (mainMenu == 3) // edit an expense 
                {
                    // code here
                }
                else if (mainMenu == 4) // remove an expense 
                {
                    // code here
                }
                else if (mainMenu == 5) // remove all expenses 
                {
                    int subMenu = ShowMenu("Är du säker på att du vill ta bort ALLA utgifter?", new[]
                    {
                        "Ja",
                        "Nej"
                    });

                    if (subMenu == 0) // removes all posts in Expenses
                    {
                        Expenses.Clear();
                    }
                    // if subMenu == 1 control falls out to main-loop again 
                }
                else
                {
                    Console.Write("Exiting program. Goodbye!");
                    Console.WriteLine();
                    break; // breaks main-loop 
                }
            } // <-- end of main-loop 
        }

        public static void AddExpense()
        {
            Console.Write("Namn: ");
            string name = Console.ReadLine();
            Console.Write("Pris: ");
            decimal price = decimal.Parse(Console.ReadLine());

            string category = "";
            decimal vat = 0.0m;

            int categoryChoice = ShowMenu("Kategori", new[]
            {
                        "Utbildning",
                        "Böcker,",
                        "Livsmedel",
                        "Övrigt"
            });

            if (categoryChoice == 0)
            {
                category = "Utbildning";
                vat = GetVAT(category);
            }
            else if (categoryChoice == 1)
            {
                category = "Böcker";
                vat = GetVAT(category);
            }
            else if (categoryChoice == 2)
            {
                category = "Livsmedel";
                vat = GetVAT(category);
            }
            else
            {
                category = "Övrigt";
                vat = GetVAT(category);
            }

            Expense expense = new Expense
            {
                Name = name,
                Price = price,
                Category = category,
                VAT = vat
            };
            Expenses.Add(expense);

            Console.Clear();
        }

        public static void ListExpenses(List<Expense> expenses)
        {
            // public static List<Expense> ListExpenses(string category) {  }
            if (Expenses.Count == 0)
            {
                Console.WriteLine("Du har inte lagt till några utgifter ännu");
            }
            else
            {
                Console.WriteLine("Utgifter: ");
                Console.WriteLine();
                foreach (Expense expense in expenses)
                {
                    Console.WriteLine(expense.Name + ": " + expense.Price.ToString("0.00") + 
                        "kr (" + expense.Category + ") ");
                    
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("Antal utgifter: " + expenses.Count);
                  //  Console.WriteLine("Summa: " + );
                    Console.WriteLine("");
                    Console.WriteLine("");
                }
            }
        }


        // Return the sum of all expenses in the specified list, with or without VAT based on the
        // second parameter. This method *must* be in the program and *must* be used in
        // both the main program and in the tests.
        public static decimal SumExpenses(List<Expense> expenses, bool includeVAT)
        {
            decimal sum = 0;
            // Implement the rest of this method here.

            foreach (Expense expense in expenses)
            {
                if (includeVAT)
                {
                    sum += expense.Price * (1 - GetVAT(expense.Category));
                }
                else
                {
                    sum += expense.Price;
                }
            }

            return sum;
        }

        // method to get VAT associated with category
        public static decimal GetVAT(string category)
        {
            decimal rValue = 0.0m;
            switch (category)
            {
                case "Utbildning":
                    // Change nothing since rValue is already 0 
                    break;

                case "Böcker":
                    rValue = 0.06m; // books = 6% VAT
                    break;

                case "Livsmedel":
                    rValue = 0.12m; // food = 12% VAT
                    break;

                case "Övrigt":
                    rValue = 0.25m; // other = 25% VAT
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
            decimal expected = 0.0m;
            decimal result = Program.GetVAT(testInput);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetVATBooks()
        {
            string testInput = "Böcker";
            decimal expected = 0.06m;
            decimal result = Program.GetVAT(testInput);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetVATFood()
        {
            string testInput = "Livsmedel";
            decimal expected = 0.12m;
            decimal result = Program.GetVAT(testInput);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetVATOther()
        {
            string testInput = "Övrigt";
            decimal expected = 0.25m;
            decimal result = Program.GetVAT(testInput);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void GetVATEmptyParameter()
        {
            string testInput = "";
            decimal expected = 0.0m;
            decimal result = Program.GetVAT(testInput);
            Assert.AreEqual(expected, result);
        }
    }
}