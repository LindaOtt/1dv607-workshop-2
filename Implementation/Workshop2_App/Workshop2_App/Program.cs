using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop2_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello User");
            //string message = "First line 4.\r\nSecond line.\r\nThird line.";

            
        }

     

        public void writeToFile(string message)
        {
            // Write the string to a file.
            //System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Users\\Fam\\Google Drive\\Webbprogrammerare\\1DV607 Objektorienterad analys och design med UML\\Workshop II\\Implementation\\Workshop2_App\\Workshop2_App\\registry.txt", false);
            System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\registry.txt", false);
            file.WriteLine(message);

            file.Close();
        }
    }
}
