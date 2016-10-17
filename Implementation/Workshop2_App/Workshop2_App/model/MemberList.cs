using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop2_App.model
{
    class MemberList
    {

        //List to store the members in
        private List<Member> members = new List<Member>();

        public MemberList()
        {
            getMembersFromDb();
        }

        //Gets the members from the text file and puts them into the member list
        public void getMembersFromDb()
        {
            //Read the text file
            string line;
            Boolean membersFound = false;
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\\data\\registry.txt");
            while ((line = file.ReadLine()) != null)
            {
                //System.Console.WriteLine(line);
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
                else
                {
                    if (membersFound)
                    {
                        //Creating a new Member and adding it to the list
                        Member member = new Member();

                        //Getting the name of the member
                        string[] stringSeparators = new string[] { ", " };
                        string[] result;

                        result = line.Split(stringSeparators, StringSplitOptions.None);

                        int counter = 1;
                        foreach (string s in result)
                        {
                            //Console.Write("'{0}' ", String.IsNullOrEmpty(s) ? "<>" : s);

                            if (counter == 1)
                            {
                                //Adding the name to the member
                                member.Name = s;
                            }
                            else if (counter == 2)
                            {
                                //Adding the personal number to the member
                                member.PersonalNumber = s;
                            }
                            else if (counter == 3)
                            {
                                //Adding the unique id to the member
                                member.UniqueId = s;
                            }
                            counter++;
                        }

                        //Adding the member to the list
                        members.Add(member);
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
    }
}

