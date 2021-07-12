using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //sql_connection X = new sql_connection();
            //Console.WriteLine(DateTime.Now.ToString("d"));

            menus_Functions Y = new menus_Functions();

            Y.menuMain(1);
            

            Console.ReadLine();

        }
    }
}
