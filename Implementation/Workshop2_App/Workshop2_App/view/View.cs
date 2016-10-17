using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2_App.model;

namespace Workshop2_App.view
{
    class View
    {
        private List<Member> members = new List<Member>();

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
            
            Console.WriteLine("\nYou\'ve chosen to view all members in a compact list.");

            //Getting the memberlist from MemberList class
            MemberList memberList = new MemberList();

            members = memberList.getMemberList();

            //Writing out the members 
            foreach (Member member in members)
            {
                Console.WriteLine("\nMember name: {0}, Personal number: {1}, UniqueId: {2}", member.Name, member.PersonalNumber, member.UniqueId);
            }
        }

        public void viewListAllVerbose()
        {
            Console.WriteLine("\nYou've chosen to view all members in a verbose list.");
        }

        public void viewCreateNewMember()
        {
            Console.WriteLine("\nYou've chosen to create a new member.");
        }

        public void viewChangeMember()
        {
            Console.WriteLine("\nYou've chosen to change a member");
        }

        public void viewLookAtMember()
        {
            Console.WriteLine("\nYou've chosen to look at a member");
        }

        public void viewRegisterNewBoat()
        {
            Console.WriteLine("\nYou've chosen to register a new boat");
        }

        public void viewDeleteBoat()
        {
            Console.WriteLine("\nYou've chosen to delete a boat");
        }

        public void viewChangeBoat()
        {
            Console.WriteLine("\nYou've chosen to change a boat");
        }

        public void viewWrongInput()
        {
            Console.WriteLine("\nYou've entered an option that doesn't exist. Please try again.");
        }
    }
}
