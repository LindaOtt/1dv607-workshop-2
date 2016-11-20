using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Workshop2_App.model
{
    class MemberList
    {

        //List to store the members in
        private List<Member> members = new List<Member>();

        //List to store the boats for the member in
        //private List<Boat> memberBoats = new List<Boat>();

        public MemberList()
        {
            getMembersFromDb();
        }

        //Gets the members from the text file and puts them into the member list
        public void getMembersFromDb()
        {
            //Read the text file
            string line;
            bool membersFound = false;
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\\data\\registry.txt");
            while ((line = file.ReadLine()) != null)
            {
                //Getting the "Members" part
                if (line == "#Members")
                {
                    membersFound = true;
                }
                
                else if (line == "#")
                {
                    membersFound = false;
                    break;
                }
                else if (line == "##")
                {
                    membersFound = false;
                }
                else if (line == "")
                {
                    continue;
                }
                else
                {
                    if (membersFound)
                    {
                        //Creating a new Member
                        Member member = new Member();

                        //Getting the properties of the member from the database (text file)
                        string[] stringSeparators = new string[] { ", " };
                        string[] result;

                        result = line.Split(stringSeparators, StringSplitOptions.None);

                        int counter = 1;
                        foreach (string s in result)
                        {

                            if (counter == 1)
                            {
                                //Adding the unique id to the member
                                member.UniqueId = s;
                            }
                            else if (counter == 2)
                            {
                                //Adding the name to the member
                                member.Name = s;
                            }
                            else if (counter == 3)
                            {
                                //Adding the personal number to the member
                                member.PersonalNumber = s;
                            }
                            counter++;
                        }

                        //Adding the boatlist to the member
                        BoatList boatList = new BoatList(member.UniqueId);
                        member.MemberBoats = boatList.getBoatList();

                        //Adding the member to the list
                        members.Add(member);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            file.Close();
        }

        //Returns the list of boats
        public List<Member> getMemberList()
        {
            return members;
        }

        public string getUniqueId(int memberNumber)
        {

            Debug.WriteLine("Inside getUniqueId");
            string uniqueId = "";

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
    }
}

