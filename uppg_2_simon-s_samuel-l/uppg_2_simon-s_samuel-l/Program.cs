using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

// Assignment nr. 2 
// by Simon Sörqvist & Samuel Lööf 

/*
 * Check before turning in: 
 * 
 * Removed unnecessary comments? i.e. Jakobs instruktioner?
 * Write tests for sumexpenses 
 */

/*
 * Lägg till typ detta när användaren ändrar en utgift :
 * 
 * [användaren väljer "päron"]

    Vad vill du ändra?
    - Namn
    - Pris
    - Kategori
 */

namespace ExpenseTracker
{
    public class Expense
    {
        // Add variables here.
        public string Name;
        public string Category;
        public decimal Price;
    }

    public class Program
    {
        // Static List to hold all expenses throughout the program
        public static List<Expense> Expenses = new List<Expense>();

        // Static dictionary to hold values related to their respective VAT 
        static Dictionary<string, decimal> CategoryVAT = new Dictionary<string, decimal>();

        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            // Write the main program code here.

            CategoryVAT.Add("Utbildning", 0.00m);
            CategoryVAT.Add("Böcker", 0.06m);
            CategoryVAT.Add("Livsmedel", 0.12m);
            CategoryVAT.Add("Övrigt", 0.25m);

            // welcome message, runs once on start 
            Console.WriteLine("Välkommen!");
            Console.WriteLine();

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
                    ListExpenses(Expenses);
                }
                else if (mainMenu == 2) // show sum per category 
                {
                    ShowSumPerCategory();
                }
                else if (mainMenu == 3) // edit an expense 
                {
                    if  (Expenses.Count == 0)
                    {
                        Console.WriteLine("Det finns inga utgifter att redigera");
                    }
                    else 
                    {
                        string[] expenseInfo = new string[Expenses.Count];
                        for (int i = 0; i < Expenses.Count; i++ )
                        {
                            expenseInfo[i] = $"{Expenses[i].Name}: {Expenses[i].Price.ToString("0.00")} kr ({Expenses[i].Category})";
                        }
                        
                        int editMenu = ShowMenu("Vilken utgift vill du redigera?", expenseInfo);

                        Console.Clear();

                        Console.WriteLine($"Du redigerar: {expenseInfo[editMenu]}");
                        Console.WriteLine();

                        AddExpense();
                        Expenses.RemoveAt(editMenu);

                        Console.WriteLine("Utgiften " +
                            $"{expenseInfo[editMenu].Substring(0, expenseInfo[editMenu].IndexOf(':'))}" +
                            " har ändrats.");
                    }
                    Console.Clear();
                }
                else if (mainMenu == 4) // remove an expense 
                {
                    if (Expenses.Count == 0)
                    {
                        Console.WriteLine("Det finns inga utgifter att ta bort.");
                    }
                    else
                    {
                        string[] expenseInfo = new string[Expenses.Count];
                        for (int i = 0; i < Expenses.Count; i++)
                        {
                            expenseInfo[i] = $"{Expenses[i].Name}: {Expenses[i].Price.ToString("0.00")} kr ({Expenses[i].Category})";
                        }

                        int removeMenu = ShowMenu("Välj utgift att ta bort:", expenseInfo);

                        Console.Clear();

                        int sureMenu = ShowMenu("Är du säker?", new[]
                        {
                            "Ja",
                            "Nej"
                        });

                        if (sureMenu == 0)
                        {
                            Expenses.RemoveAt(removeMenu);
                        }
                        // else = returns to main-loop

                        Console.Clear();

                        Console.WriteLine("Utgiften " + 
                            $"{expenseInfo[removeMenu].Substring(0, expenseInfo[removeMenu].IndexOf(':'))}" +
                            " har tagits bort.");
                        Console.WriteLine();
                    }
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
                        Console.Clear();
                        Console.WriteLine("Samtliga utgifter har tagits bort.");
                        Console.WriteLine();
                    }
                    // if subMenu == 1 control falls out to main-loop again 
                }
                else
                {
                    Console.Write("Avslutar programmet, hejdå!");
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
                        "Böcker",
                        "Livsmedel",
                        "Övrigt"
            });

            if (categoryChoice == 0)
            {
                category = "Utbildning";
                vat = CategoryVAT[category];
            }
            else if (categoryChoice == 1)
            {
                category = "Böcker";
                vat = CategoryVAT[category];
            }
            else if (categoryChoice == 2)
            {
                category = "Livsmedel";
                vat = CategoryVAT[category];
            }
            else
            {
                category = "Övrigt";
                vat = CategoryVAT[category];
            }

            Expense expense = new Expense
            {
                Name = name,
                Price = price,
                Category = category,
            };

            Expenses.Add(expense);

            Console.Clear();

            Console.WriteLine("Utgift tillagd/redigerad.");
            Console.WriteLine();
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
                }
                decimal price = SumExpenses(expenses, true);
                decimal priceVat = SumExpenses(expenses, false);
                Console.WriteLine("");
                Console.WriteLine("Antal utgifter: " + expenses.Count);
                Console.WriteLine("Summa: " + price.ToString("0.00") + " kr (" + priceVat.ToString("0.00") + " kr exkl. moms)");
                    Console.WriteLine("");
            }
        }
        
        public static void ShowSumPerCategory()
        {
            List<Expense> foodList = new List<Expense>();
            List<Expense> educationList = new List<Expense>();
            List<Expense> booksList = new List<Expense>();
            List<Expense> otherList = new List<Expense>();

            foreach (Expense expense in Expenses)
            {
                if (expense.Category == "Livsmedel")
                {
                    foodList.Add(expense);
                }
                else if (expense.Category == "Utbildning")
                {
                    educationList.Add(expense);
                }
                else if (expense.Category == "Böcker")
                {
                    booksList.Add(expense);
                }
                else
                {
                    otherList.Add(expense);
                }
            }
            Console.WriteLine("Summa per Kategori: ");
            Console.WriteLine("");

            Console.WriteLine($"Utbildning: {SumExpenses(educationList, true).ToString("0.00")} kr " +
                $"({SumExpenses(educationList, false).ToString("0.00")} kr exkl. moms)");

            Console.WriteLine($"Böcker: {SumExpenses(booksList, true).ToString("0.00")} kr " +
                $"({SumExpenses(booksList, false).ToString("0.00")} kr exkl. moms)");

            Console.WriteLine($"Livsmedel: {SumExpenses(foodList, true).ToString("0.00")} kr " +
                $"({SumExpenses(foodList, false).ToString("0.00")} kr exkl. moms)");

            Console.WriteLine($"Övrigt: {SumExpenses(otherList, true).ToString("0.00")} kr " +
                $"({SumExpenses(otherList, false).ToString("0.00")} kr exkl. moms)");

            Console.WriteLine("");
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
                    sum += expense.Price;
                }
                else
                {
                    sum += expense.Price / (1 + CategoryVAT[expense.Category]);
                }
            }

            return sum;
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
    }
}