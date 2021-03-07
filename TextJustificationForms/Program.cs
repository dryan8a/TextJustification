using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextJustificationForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Rope rope = new Rope();
            rope.Insert("World", 0);
            rope.Append("!");
            rope.Insert("Hello", 0);
            rope.Insert(" ",5);
            for(int i = 0;i<13;i++)
            {
                Console.Write(rope[i]);
            }
            ;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
