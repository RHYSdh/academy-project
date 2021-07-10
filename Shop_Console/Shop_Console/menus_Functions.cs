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

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                menuNew(1);
            }
            else if (choice == "2")
            {
                menuReports(1);
            }
            else if (choice == "10")
            {
                System.Environment.Exit(0);
            }

            else
            {
                menuMain();
            }
        }

        public void menuNew(int chk = 0) //Menu to add new sale entry
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
            float price = 0;
            string setDate = "";
            float defPrice = 1.00F;
            int value;

            string choice = Console.ReadLine();

            if (choice == "1") //If Listed product
            {

                string[] drinks = { "Pepsi", "Coke", "Evian", "Tango", "7up", "Dr Pepper", "Sprite", "Mountain Dew", "Monster", "Redbull", "Fanta" };
                float[] prices = { 0.99F, 1.20F, 0.70F, 0.89F, 1.00F, 1.10F, 0.99F, 1.50F, 2.10F, 2.00F, 1.20F };

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
                    choice = Console.ReadLine();

                    if (int.TryParse(choice, out value)) //If input is a number
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
                            menuNew(1);
                        }
                        else { Console.WriteLine("Invalid entry, try again: "); }
                    }
                    else { Console.WriteLine("Invalid entry, try again: "); }
                }
            }
            else if (choice == "2") // If freeform product
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
            else if (choice == "0") // Exit to main menu
            {
                menuMain(1);
            }
            else //Invalid input reload menu with default error
            {
                menuNew();
            }

            //Start additional questions (Quantity)

            Console.WriteLine("");
            Console.Write("How many sold? ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Default is 1: ");
            Console.ResetColor();

            while (qty == 0)
            {
                choice = Console.ReadLine();
                if (int.TryParse(choice, out value))
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
                    if (choice == "")
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
                choice = Console.ReadLine();
                float p;
                if (float.TryParse(choice, out p))
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

                    if (choice == "")
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
                choice = Console.ReadLine();

                if (choice == "")
                {
                    setDate = DateTime.Now.ToString("yyyy-MM-dd");
                    userDate = DateTime.Now.ToString("d");
                }
                else
                {
                    if (choice.Length == 10)
                    {
                        string day = choice.Substring(0, 2);
                        string month = choice.Substring(3, 2);
                        string year = choice.Substring(6, 4);

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
                                    if(DateTime.TryParse(setDate, out temp))
                                    {
                                        userDate = setDate;
                                    }
                                    else
                                    {
                                        Console.WriteLine(choice + " is not a valid date, try again:");
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

            Console.WriteLine("Confirm update (Y/N):");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{product} Qty:{qty} @ £{price} Date:{userDate}");
            Console.ResetColor();
            string confirmed = "";
            while(confirmed == "")
            {
                confirmed = Console.ReadLine();
                if(confirmed == "Y" || confirmed == "y")
                {
                    SQL.SQLCreate(product, qty, price, setDate);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Added");
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(750);
                }
                else if(confirmed == "N" || confirmed == "n")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Not added");
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(750);
                }
                else
                {
                    Console.WriteLine("Invalid entry, try again: ");
                    confirmed = "";
                }
            }
            
            menuNew(1);

        }

        public void menuReports(int chk = 0) //Menu to check pre-set reports
        {
            newHeader("Review Sales", ConsoleColor.Green);
            Console.WriteLine("");
            Console.WriteLine("Choose an option and press enter:");
            Console.WriteLine("");
            Console.WriteLine("1. ");
            Console.WriteLine("2. ");
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

            int choice = Int32.Parse(Console.ReadLine());

            if (choice == 10)
            {
                menuMain(1);
            }
            else
            {
                menuReports();
            }
        }

    }
}
