using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Console
{
    class menus_Functions
    {
        sql_connection SQL = new sql_connection();
        public void newHeader(string title, ConsoleColor colour) // Create heading for menu
        {
            Console.Clear();

            title = title.PadRight(17 + (title.Length / 2), ' ');
            title = title.PadLeft(35, ' ');

            Console.ForegroundColor = colour;
            Console.WriteLine("*****************************************");
            Console.WriteLine("*****************************************");
            Console.WriteLine("**                                     **");
            Console.WriteLine("** " + title + " **");
            Console.WriteLine("**                                     **");
            Console.WriteLine("*****************************************");
            Console.WriteLine("*****************************************");
            Console.ResetColor();
        }

        public void menuMain(int chk = 0) //Main menu
        {
            newHeader("Shopping System ", ConsoleColor.DarkCyan);
            Console.WriteLine("");
            Console.WriteLine("Choose an option and press enter:");
            Console.WriteLine("");
            Console.WriteLine("1. New Entry");
            Console.WriteLine("2. Reports");
            Console.WriteLine("");
            Console.WriteLine("10. Exit");
            Console.WriteLine("");

            if (chk == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid choice, try again:");
                Console.WriteLine("");
                Console.ResetColor();
            }

            string mainChoice = Console.ReadLine();

            if (mainChoice == "1")
            {
                menuNewEntry(1);
            }
            else if (mainChoice == "2")
            {
                menuReports(1);
            }
            else if (mainChoice == "10")
            {
                System.Environment.Exit(0);
            }
            else
            {
                menuMain();
            }
        }

        public void menuNewEntry(int chk = 0) //Menu to add new sale entry
        {
            newHeader("Add new entry", ConsoleColor.Green);
            Console.WriteLine("");
            Console.WriteLine("Choose an option and press enter:");
            Console.WriteLine("");
            Console.WriteLine("1. Listed product");
            Console.WriteLine("2. Free-text product");
            Console.WriteLine("");
            Console.WriteLine("0. Back");
            Console.WriteLine("");

            if (chk == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid choice, try again:");
                Console.WriteLine("");
                Console.ResetColor();
            }

            string product = "";
            int qty = 0;
            decimal price = 0;
            string setDate = "";
            decimal defPrice = 1.00M;
            int value;

            string newChoice = Console.ReadLine();

            if (newChoice == "1") //If Listed product
            {

                string[] drinks = { "Pepsi", "Coke", "Evian", "Tango", "7up", "Dr Pepper", "Sprite", "Mountain Dew", "Monster", "Redbull", "Fanta" };
                decimal[] prices = { 0.99M, 1.20M, 0.70M, 0.89M, 1.00M, 1.10M, 0.99M, 1.50M, 2.10M, 2.00M, 1.20M };

                newHeader("Add new entry", ConsoleColor.Green);
                Console.WriteLine("");
                Console.WriteLine("Choose an option and press enter:");
                Console.WriteLine("");
                for (int i = 1; i <= 11; i++) // print array
                {
                    Console.WriteLine($"{i}. {drinks[i - 1]}");
                }
                Console.WriteLine("");
                Console.WriteLine("0. Back");
                Console.WriteLine("");

                while (product == "") //Wait for correct input
                {
                    newChoice = Console.ReadLine();

                    if (int.TryParse(newChoice, out value)) //If input is a number
                    {
                        if (value >= 1 && value <= 11)
                        {
                            product = drinks[value - 1];
                            defPrice = prices[value - 1];
                            newHeader("Add new entry", ConsoleColor.Green);
                            Console.WriteLine("");
                            Console.WriteLine("Press enter to accept the default value or");
                            Console.WriteLine("type an answer and press enter:");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("");
                            Console.WriteLine(product);
                            Console.ResetColor();
                        }
                        else if (value == 0)
                        {
                            menuNewEntry(1);
                        }
                        else { Console.WriteLine("Invalid entry, try again: "); }
                    }
                    else { Console.WriteLine("Invalid entry, try again: "); }
                }
            }
            else if (newChoice == "2") // If freeform product
            {
                newHeader("Add new entry", ConsoleColor.Green);
                Console.WriteLine("");
                Console.WriteLine("Type the product name:");
                while (product == "") // Valid input between 1 & 45
                {
                    string entry = Console.ReadLine();

                    if (entry.Length >= 1 && entry.Length <= 45) { product = entry; }
                    else if (entry.Length > 45) { Console.WriteLine("Entry too long, try again:"); }
                    else { Console.WriteLine("Invalid entry try again: "); }
                }
                Console.WriteLine("");
                Console.WriteLine("Press enter to accept the default value or");
                Console.WriteLine("type an answer and press enter:");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("");
                Console.WriteLine(product);
                Console.ResetColor();
            }
            else if (newChoice == "0") // Exit to main menu
            {
                menuMain(1);
            }
            else //Invalid input reload menu with default error
            {
                menuNewEntry();
            }

            //If product has been set, start additional questions.
            if (product != "") //Additional questions (Quantity)
            {
                Console.WriteLine("");
                Console.Write("How many sold? ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Default is 1: ");
                Console.ResetColor();

                while (qty == 0)
                {
                    newChoice = Console.ReadLine();
                    if (int.TryParse(newChoice, out value))
                    {
                        if (value == 0)
                        {
                            Console.WriteLine("Input must be at least 1");
                        }
                        else
                        {
                            qty = value;
                        }
                    }
                    else
                    {
                        if (newChoice == "")
                        {
                            qty = 1;
                        }
                        else
                        {
                            Console.WriteLine("Invalid entry, try again: ");
                        }

                    }
                }

                // Additional questions (Price)

                Console.WriteLine("");
                Console.Write("How much? (X.XX) ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"Default is £{defPrice}: ");
                Console.ResetColor();

                while (price == 0)
                {
                    newChoice = Console.ReadLine();
                    decimal p;
                    if (decimal.TryParse(newChoice, out p))
                    {
                        if (p == 0)
                        {
                            Console.WriteLine("Input must be over 0");
                        }
                        else
                        {
                            price = p;
                        }
                    }
                    else
                    {

                        if (newChoice == "")
                        {
                            price = defPrice;
                        }
                        else
                        {
                            Console.WriteLine("Invalid entry, try again: ");
                        }
                    }
                }

                // Additional questions (Date)

                Console.WriteLine("");
                Console.Write("Date? (DD/MM/YYYY) ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"Default is {DateTime.Now.ToString("d")}: ");
                Console.ResetColor();
                string userDate = "";

                while (setDate == "")
                {
                    newChoice = Console.ReadLine();

                    if (newChoice == "")
                    {
                        setDate = DateTime.Now.ToString("yyyy-MM-dd");
                        userDate = DateTime.Now.ToString("d");
                    }
                    else
                    {
                        if (newChoice.Length == 10)
                        {
                            string day = newChoice.Substring(0, 2);
                            string month = newChoice.Substring(3, 2);
                            string year = newChoice.Substring(6, 4);

                            if (int.TryParse(day, out value))
                            {
                                setDate = day;
                                if (int.TryParse(month, out value))
                                {
                                    setDate = month + "-" + setDate;
                                    if (int.TryParse(year, out value))
                                    {
                                        setDate = year + "-" + setDate;
                                        DateTime temp;
                                        if (DateTime.TryParse(setDate, out temp))
                                        {
                                            userDate = setDate;
                                        }
                                        else
                                        {
                                            Console.WriteLine(newChoice + " is not a valid date, try again:");
                                            setDate = "";
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error with Year, (format DD/MM/YYYY), (example 01/01/2001), try again:");
                                        setDate = "";
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error with Month, (format DD/MM/YYYY), (example 01/01/2001), try again:");
                                    setDate = "";
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error with Day, (format DD/MM/YYYY), (example 01/01/2001), try again:");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error with Date, (format DD/MM/YYYY), (example 01/01/2001), try again:");
                        }
                    }
                }

                // Additional questions (Confirm new entry)
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{product} Qty:{qty} @ £{price} Date:{userDate}");
                Console.ResetColor();

                Console.WriteLine("");
                Console.WriteLine("Confirm new entry? (Y/N)");
                Console.WriteLine("");

                string confirmed = "";
                while (confirmed == "")
                {
                    confirmed = Console.ReadLine();
                    if (confirmed == "Y" || confirmed == "y")
                    {
                        SQL.SQLCreate(product, qty, price, setDate);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Added");
                        Console.ResetColor();
                    }
                    else if (confirmed == "N" || confirmed == "n")
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Not added");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("Invalid entry, try again: ");
                        confirmed = "";
                    }
                }

                System.Threading.Thread.Sleep(750);
                menuNewEntry(1);
            }
        }

        public void menuReports(int chk = 0) //Menu to check pre-set reports
        {
            newHeader("Review Sales", ConsoleColor.Green);
            Console.WriteLine("");
            Console.WriteLine("Choose an option and press enter:");
            Console.WriteLine("");
            Console.WriteLine("1. Products sold by year");
            Console.WriteLine("2. Products sold by month");
            Console.WriteLine("3. Total sales by year");
            Console.WriteLine("4. Total sales by month");
            Console.WriteLine("");
            Console.WriteLine("10. Back");
            Console.WriteLine("");

            if (chk == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid choice, try again:");
                Console.WriteLine("");
                Console.ResetColor();
            }

            string repChoice = Console.ReadLine();

            if (repChoice == "1")
            {
                getReport("n", "n");
            }
            else if (repChoice == "2")
            {
                getReport("y", "n");
            }
            else if (repChoice == "3")
            {
                getReport("n", "y");
            }
            else if (repChoice == "4")
            {
                getReport("y", "y");
            }
            else if (repChoice == "10")
            {
                menuMain(1);
            }
            else
            {
                menuReports();
            }
        }

        public void getReport(string monthNeeded, string totalNeeded)
        {

            string year = "";
            string month = "";

            newHeader("Review Sales", ConsoleColor.Green);

            Console.WriteLine("");
            Console.WriteLine("Type the year in (YYYY) format and press enter:");
            Console.WriteLine("");

            while (year == "")
            {
                year = Console.ReadLine();
                int temp;
                if (int.TryParse(year, out temp) && year.Length == 4)
                {
                }
                else
                {
                    Console.WriteLine("Invalid entry, try again:");
                    year = "";
                }
            }

            if (monthNeeded == "y")
            {
                while (month == "")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Type the month number in (1-12) and press enter:");
                    Console.WriteLine("");
                    month = Console.ReadLine();
                    int temp;
                    if (int.TryParse(month, out temp))
                    {
                        if (Int32.Parse(month) >= 1 && Int32.Parse(month) <= 12)
                        {
                        }
                        else
                        {
                            Console.WriteLine("Invalid entry, try again: ");
                            month = "";
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid entry, try again: ");
                        month = "";
                    }

                }
            }
            else
            {
                month = "12";
            }

            if (totalNeeded == "y")
            {

                if (monthNeeded == "y")
                {
                    Console.WriteLine($" Product        Quantity       Price          Total, for  {month}/{year}");
                    Console.WriteLine("");
                    Console.WriteLine(SQL.SQLReturn("productname,count(*),price,price*quantity", 4, $"where year(saledate)='{year}' and month(saledate)='{month}' group by productname order by 4 desc;", 14));
                    Console.WriteLine("Total £" + SQL.SQLReturn("sum(price * quantity)", 1, $"where year(saledate)='{year}' and month(saledate)='{month}'", 5));
                }
                else
                {
                    Console.WriteLine($" Product        Quantity       Price          Total, for year {year}");
                    Console.WriteLine("");
                    Console.WriteLine(SQL.SQLReturn("productname,count(*),price,price*quantity", 4, $"where year(saledate)='{year}' group by productname order by 4 desc;", 14));
                    Console.WriteLine("Total £" + SQL.SQLReturn("sum(price * quantity)", 1, $"where year(saledate)={year}", 5));
                }
            }
            else
            {
                if (monthNeeded == "y")
                {
                    Console.WriteLine($"Sales / Product for {month}/{year}");
                    Console.WriteLine("");
                    Console.WriteLine(SQL.SQLReturn("count(*),productname", 2, $"where year(saledate)='{year}' and month(saledate)='{month}' group by productname order by 1 desc;", 6));
                }
                else
                {
                    Console.WriteLine("Sales / Product for year " + year);
                    Console.WriteLine("");
                    Console.WriteLine(SQL.SQLReturn("count(*),productname", 2, $"where year(saledate)='{year}' group by productname order by 1 desc;", 6));
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            menuReports(1);
        }
    }
}
