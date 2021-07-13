using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;

namespace Shop_Console
{
    class sql_connection
    {
        //Update these variables to connect to your MySQL server / table
        string server = "localhost";
        string user = "root";
        string password = "root";
        string database = "sales";
        string tableName = "sales";


        
        MySqlConnection con;
        MySqlCommand cmd;

        public sql_connection() // Constructor makes connection to MySQL database
        {
            string connString = $"server={server};user={user};password={password};database={database}";
            try
            {
                con = new MySqlConnection(connString);
                con.Open();
                cmd = new MySqlCommand();
                cmd.Connection = con;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("***ERROR*** With MySQL connection ***ERROR***");
                Console.ResetColor();
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }

        public string SQLReturn(string what, int whatLength, string where, int padding) //return string of SQL select command  
        {
            string input = $"select {what} from {tableName} {where}";
            string output = "";

            cmd.CommandText = input;
            MySqlDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                for (int i = 0; i < whatLength; i++)
                {
                    output += " " + data[i].ToString().PadRight(padding, ' ');
                }
                output += "\n";
            }
            data.Close();
            return output;
        } //"Select -what- from -table- where -xyz-", Use ";" if no where option is needed, whatLength must be <= MySQL table columns

        public void SQLCreate(string product, int qty, decimal price, string inDate) //insert data to shop table
        {
            cmd.CommandText = $"INSERT INTO {tableName}(ProductName, Quantity, Price, SaleDate) VALUES ('{product}',{qty},{price},'{inDate}')";
            cmd.ExecuteNonQuery();
        }

        public void massInsert(int howMany) //populate table with random data
        {
            Random rand = new Random();

            for (int i = 0; i < howMany; i++)
            {
                int dat1 = rand.Next(1, 32);
                int dat2 = rand.Next(1, 13);
                int dat3 = rand.Next(2019, 2022);
                int prod1 = rand.Next(1, 12);
                string prodName = "";
                int qty1 = rand.Next(1, 5);
                decimal price = 0;

                if (dat1 == 31 && dat2 == 2 || dat1 == 30 && dat2 == 2 || dat1 == 29 && dat2 == 2)
                {
                    dat1 = dat1 - 10;
                }

                if (dat1 == 31 && dat2 == 9 || dat1 == 31 && dat2 == 4 || dat1 == 31 && dat2 == 6 || dat1 == 31 && dat2 == 11)
                {
                    dat1 = dat1 - 1;
                }

                if (prod1 == 1)
                {
                    prodName = "Pepsi";
                    price = 0.99M;
                }
                else if (prod1 == 2)
                {
                    prodName = "Coke";
                    price = 1.20M;
                }
                else if (prod1 == 3)
                {
                    prodName = "Evian";
                    price = 0.70M;
                }
                else if (prod1 == 4)
                {
                    prodName = "Tango";
                    price = 0.89M;
                }
                else if (prod1 == 5)
                {
                    prodName = "7up";
                    price = 1.00M;
                }
                else if (prod1 == 6)
                {
                    prodName = "Dr Pepper";
                    price = 1.10M;
                }
                else if (prod1 == 7)
                {
                    prodName = "Sprite";
                    price = 0.99M;
                }
                else if (prod1 == 8)
                {
                    prodName = "Mountain Dew";
                    price = 1.50M;
                }
                else if (prod1 == 9)
                {
                    prodName = "Monster";
                    price = 2.10M;
                }
                else if (prod1 == 10)
                {
                    prodName = "Redbull";
                    price = 2.00M;
                }
                else if (prod1 == 11)
                {
                    prodName = "Fanta";
                    price = 1.20M;
                }
                else
                {
                    prodName = "Coffee";
                    price = 1.00M;
                }
                SQLCreate(prodName, qty1, price, $"{dat3}-{dat2}-{dat1}");
                Console.WriteLine($"({prodName}, {qty1}, {price}, {dat3}-{dat2}-{dat1})");
            }
        }

    }
}



