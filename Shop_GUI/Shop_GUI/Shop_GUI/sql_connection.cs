using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;

namespace Shop_GUI
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

    }
}



