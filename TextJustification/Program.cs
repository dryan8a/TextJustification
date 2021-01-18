using System;
using System.Text;

namespace TextJustification
{

    class Program
    {
        
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string output = JustifyText(input, 25);
            Console.WriteLine(output);
        }
    }
}
