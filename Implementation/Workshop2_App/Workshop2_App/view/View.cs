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
            Console.Clear();
            Console.WriteLine("Hello User! Type a letter (A to H) to take your pick:");
            Console.WriteLine("(Press \"0\" key to quit)");
            Console.WriteLine("A. List all members (compact view)");
            Console.WriteLine("B. List all members (verbose view)");
            Console.WriteLine("C. Create a new member");
            Console.WriteLine("D. Change a member's information");
            Console.WriteLine("E. Look at a member's information");
            Console.WriteLine("F. Register a new boat for a member");
            Console.WriteLine("G. Delete a boat");
            Console.WriteLine("H. Change a boat's information");
        }

        public void viewListAllCompact()
        {
            Console.WriteLine("You've chosen to view all members in a compact list.");
        }

        public void viewListAllVerbose()
        {
            Console.WriteLine("You've chosen to view all members in a verbose list.");
        }

        public void viewCreateNewMember()
        {
            Console.WriteLine("You've chosen to create a new member.");
        }

        public void viewChangeMember()
        {
            Console.WriteLine("You've chosen to change a member");
        }

        public void viewLookAtMember()
        {
            Console.WriteLine("You've chosen to look at a member");
        }

        public void viewRegisterNewBoat()
        {
            Console.WriteLine("You've chosen to register a new boat");
        }

        public void viewDeleteBoat()
        {
            Console.WriteLine("You've chosen to delete a boat");
        }

        public void viewChangeBoat()
        {
            Console.WriteLine("You've chosen to change a boat");
        }
    }
}
