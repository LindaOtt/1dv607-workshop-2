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
        private List<Member> members;
        private List<Boat> boats = new List<Boat>();
        private int numberOfBoats;

        //Getting the memberlist from MemberList class
        MemberList memberList = new MemberList();

        //Getting the boatlist from the BoatList class
        BoatList boatList = new BoatList();



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
                Console.WriteLine("---------------------------------------------------------\nMember id: {0} \nMember name: {1} \nNumber of boats: {2}", member.UniqueId, member.Name, numberOfBoats);
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
            Console.WriteLine("Enter the name of the member:");
        }


        public void viewCreateMemberEnterPNumber(Member member)
        {
            Console.WriteLine("\nYou've entered the name {0}", member.Name);
            Console.WriteLine("\nEnter the personal number of the member and click enter to save the new member:");
        }

        public void viewCreateMemberSave(Member member)
        {
            Console.WriteLine("\nYou've entered the name {0} and the personal number {1}", member.Name, member.PersonalNumber);
            Console.WriteLine("\nClick enter to save the member:");
        }

        public void newMemberCreated(Member member)
        {
            Console.WriteLine("\nYou've created a new member with the name {0}, the personal number {1} and the unique id {2}", member.Name, member.PersonalNumber, member.UniqueId);
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

        public void viewChangeMemberEnterName(Member member)
        {
            Console.WriteLine("\nYou have chosen to change the member with unique id {0}", member.UniqueId);
            Console.WriteLine("\nEnter the new name for the member: ");
            
        }

        public void viewChangeMemberEnterPNumber(Member member)
        {
            Console.WriteLine("\nYou have chosen to change the member with unique id {0} and new name {1}", member.UniqueId, member.Name);
            Console.WriteLine("\nEnter the new personal number for the member, click enter to save member: ");
        }

        public void viewChangeMemberSaved(Member member)
        {
            Console.WriteLine("\nYou have saved the member with unique id {0}, new name {1} and personal number {2}", member.UniqueId, member.Name, member.PersonalNumber);
            
        }

        public void viewLookAtMemberPick()
        {

            Console.WriteLine("\nYou've chosen to look at a member");
            Console.WriteLine("Enter the number of the member you want to look at:");

            members = memberList.getMemberList();

            int counter = 1;

            //Writing out the members 
            foreach (Member member in members)
            {
                Console.WriteLine("---------------------------------------------------------\n{0}. Name: {1} \nPersonal number: {2}", counter, member.Name, member.PersonalNumber);
                counter++;
            }
        }

        public void viewLookAtChosenMember(Member chosenMember)
        {
            Console.WriteLine("\nYou have chosen to look at the member with unique id {0}", chosenMember.UniqueId);

            members = memberList.getMemberList();

            //Writing out the members 
            foreach (Member member in members)
            {
                if (chosenMember.UniqueId == member.UniqueId) { 
                Console.WriteLine("---------------------------------------------------------\nUnique id: {0} \nName: {1} \nPersonal number: {2}", member.UniqueId, member.Name, member.PersonalNumber);
                
                }
            }

        }

       

        public void viewRegisterNewBoat()
        {
            Console.WriteLine("\nYou've chosen to register a new boat");
            Console.WriteLine("Please choose the member you want to register the boat for");

            members = memberList.getMemberList();

            int counter = 1;
            //Writing out the members 
            foreach (Member member in members)
            {
                
                Console.WriteLine("---------------------------------------------------------\n{0}. Name: {1} \nPersonal number: {2} \nMember Id: {3}", counter, member.Name, member.PersonalNumber, member.UniqueId);
                counter++;
            }

        }

        public void registerBoatEnterType(Member member, Boat boat)
        {
            Console.WriteLine("\nYou've chosen to add a boat to member {0}", member.UniqueId);
            Console.WriteLine("Choose the type of boat:");

            //Get the boat types length
            int counter = Enum.GetNames(typeof(Boat.type)).Length;
            int counterType;

            for (int i=0; i<counter; i++)
            {
                counterType = i + 1;
                Console.WriteLine("{0}." + Enum.GetName(typeof(Boat.type), i), counterType);
            }

        }

        public void registerBoatEnterLength(Member member, Boat boat)
        {
            Console.WriteLine("\nYou've chosen to add a boat to member {0}", member.UniqueId);
            Console.WriteLine("With the type {0}", boat.Type);
            Console.WriteLine("Enter the length of the boat, and click enter to save:");
        }

        public void registerBoatSaved(Member member, Boat boat)
        {
            Console.WriteLine("\nA new boat was added for member {0}, with the type {1} and length {2}", member.UniqueId, boat.Type, boat.Length);
        }

        public void viewDeleteBoat()
        {
            Console.WriteLine("\nYou've chosen to delete a boat");
            Console.WriteLine("So please pick the boat you want to delete from the list below:");
            boatList.getAllBoatsFromDb();
            boats = boatList.getBoatList();

            int counter = 1;
            //Writing out the boats 
            foreach (Boat boat in boats)
            {

                Console.WriteLine("---------------------------------------------------------\n{0}. Member Id: {1} \nBoat Type: {2} \nBoat length: {3}", counter, boat.UniqueId, boat.Type, boat.Length);
                counter++;
            }
        }

        public void viewDeleteBoatPick(Boat boat)
        {
            Console.WriteLine("You've deleted the boat with order number {0}", boat.OrderNumber ); 
        }

        public void viewChangeBoat()
        {
            Console.WriteLine("\nYou've chosen to change a boat");
            Console.WriteLine("So please pick the boat you want to change from the list below:");
            boatList.getAllBoatsFromDb();
            boats = boatList.getBoatList();

            int counter = 1;
            //Writing out the boats 
            foreach (Boat boat in boats)
            {

                Console.WriteLine("---------------------------------------------------------\n{0}. Member Id: {1} \nBoat Type: {2} \nBoat length: {3}", counter, boat.UniqueId, boat.Type, boat.Length);
                counter++;
            }
        }

        public void changeBoatEnterId(Boat boat)
        {
            Console.WriteLine("\nYou've chosen to change the boat with the order number {0}", boat.OrderNumber);
            Console.WriteLine("Enter a new member id for the boat:");
        }

        public void changeBoatEnterType(Boat boat)
        {
            Console.WriteLine("\nYou've chosen to change the boat with the order number {0} to have the member id {1}", boat.OrderNumber, boat.UniqueId);

            Console.WriteLine("Choose the type of boat and click enter to save the boat:");

            //Get the boat types length
            int counter = Enum.GetNames(typeof(Boat.type)).Length;
            int counterType;

            for (int i = 0; i < counter; i++)
            {
                counterType = i + 1;
                Console.WriteLine("{0}." + Enum.GetName(typeof(Boat.type), i), counterType);
            }
        }

        public void changeBoatEnterLength(Boat boat)
        {
            Console.WriteLine("\nYou've chosen to change the boat with the order number {0} to have the member id {1} and the type {2}", boat.OrderNumber, boat.UniqueId, boat.Type);
            Console.WriteLine("The boat has been saved.");
            
        }

        public void changeBoatSaved(Boat boat)
        {
            Console.WriteLine("You've saved the boat with the order number {0}, member id {1}, type {2} and length {3}", boat.OrderNumber, boat.UniqueId, boat.Type, boat.Length);
        }

        public void viewWrongInput()
        {
            Console.WriteLine("\nYou've entered an option that doesn't exist. Please try again.");
        }

    }
}
