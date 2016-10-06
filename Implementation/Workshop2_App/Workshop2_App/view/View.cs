using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop2_App.view
{
    class View
    {
        public void viewStart()
        {
            Console.WriteLine("Hello User! Take your pick:/");
            Console.WriteLine("1. List all members (compact view)");
            Console.WriteLine("2. List all members (verbose view)");
            Console.WriteLine("3. Create a new member");
            Console.WriteLine("4. Change a member's information");
            Console.WriteLine("5. Look at a member's information");
            Console.WriteLine("6. Register a new boat for a member");
            Console.WriteLine("7. Delete a boat");
            Console.WriteLine("7. Change a boat's information");
        }
    }
}
