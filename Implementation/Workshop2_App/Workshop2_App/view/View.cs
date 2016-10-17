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
        private List<Boat> boats = new List<Boat>();
        private int numberOfBoats;

        //Getting the memberlist from MemberList class
        MemberList memberList = new MemberList();

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

            members = memberList.getMemberList();

            //Writing out the members 
            foreach (Member member in members)
            {
                boats = member.MemberBoats;
                numberOfBoats = boats.Count;
                Console.WriteLine("---------------------------------------------------------\nMember name: {0} \nMember id: {1} \nNumber of boats: {2}", member.Name, member.UniqueId, numberOfBoats);
            }
        }

        public void viewListAllVerbose()
        {
            Console.WriteLine("\nYou've chosen to view all members in a verbose list.");

            members = memberList.getMemberList();
          

            //Writing out the members 
            foreach (Member member in members)
            {
                boats = member.MemberBoats;
                int counter = 1;
                Console.WriteLine("---------------------------------------------------------\nName: {0} \nPersonal number: {1} \nMember Id: {2}", member.Name, member.PersonalNumber, member.UniqueId);
                foreach (Boat boat in boats)
                {
                    Console.WriteLine("\nBOAT {0}: \nBoat type: {1} \nBoat length: {2}", counter, boat.Type, boat.Length);
                    counter++;
                }
            }
        }

        public void viewCreateNewMember()
        {
            Console.WriteLine("\nYou've chosen to create a new member.");
        }

        public void viewChangeMemberPick()
        {
            Console.WriteLine("\nYou've chosen to change a member");
            Console.WriteLine("Enter the number of the member you want to change:");

            members = memberList.getMemberList();

            int counter = 1;

            //Writing out the members 
            foreach (Member member in members)
            {
                Console.WriteLine("---------------------------------------------------------\n{0}. Name: {1} \nPersonal number: {2}", counter, member.Name, member.PersonalNumber);
                counter++;
            }
        }

        public void viewChangeMemberEnterName(int memberId)
        {
            Console.WriteLine("\nYou have chosen to change the member with unique id ");

            members = memberList.getMemberList();

            int counter = 1;
            foreach (Member member in members)
            {
                if(counter == memberId)
                {
                    Console.WriteLine("{0}, {1}", member.UniqueId, member.Name);
                    Console.WriteLine("\nEnter the new name for the member: ");
                }
                counter++;
            }
        }

        public void viewChangeMemberEnterPNumber(int memberId)
        {
            Console.WriteLine("\nYou have chosen to change the member with unique id ");

            members = memberList.getMemberList();

            int counter = 1;
            foreach (Member member in members)
            {
                if (counter == memberId)
                {
                    Console.WriteLine("{0}, {1}", member.UniqueId, member.Name);
                    Console.WriteLine("\nEnter the new personal number for the member: ");
                }
                counter++;
            }
        }

        public void viewChangeMemberSaved(int memberId)
        {
            Console.WriteLine("\nYou have saved the member ");

            members = memberList.getMemberList();

            int counter = 1;
            foreach (Member member in members)
            {
                if (counter == memberId)
                {
                    Console.WriteLine("{0}, {1}", member.UniqueId, member.Name);
                }
                counter++;
            }
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

        /*
        public string getUniqueId(int memberNumber)
        {
            string uniqueId="";
            members = memberList.getMemberList();

            int counter = 1;

            //Writing out the members 
            foreach (Member member in members)
            {
                if (counter == memberNumber)
                {
                    uniqueId = member.UniqueId;
                }
                counter++;
            }
            return uniqueId;
        }
        */
    }
}
