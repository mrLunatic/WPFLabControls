using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var value = 54354.554687896;
            var format = "F5";

            Console.WriteLine(value.ToString(format));
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.ReadLine();

               
        }
    }

}
