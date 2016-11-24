using System;
using System.IO;
using Workshop2_App.model;
using System.Diagnostics;

namespace Workshop2_App.controller
{
    class MemberController
    {
        private string inputFromUser;
        private string userFeedback;
        int number;
        private Member member;
        private MemberList memberList = new MemberList();

        public Member getMember()
        {
            return member;
        }

        public void setMemberFromInput(string input, string userFeedback, Member changeMember)
        {

            switch (input)
            {
                case "memberEnterName":
                    changeMember.Name = userFeedback;
                    break;
                case "memberEnterPNumber":
                
                    changeMember.PersonalNumber = userFeedback;
                    break;
                case "memberCreateSave":
                    changeMember.UniqueId = userFeedback;
                    break;
                case "memberChangeSetId":
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        if (number > 0)
                        {
                            string uniqueId = this.memberList.getUniqueId(Int32.Parse(userFeedback));
                            changeMember.UniqueId = uniqueId;
                        }
                    }
                    break;
                case "memberChangeName":
                    changeMember.Name = userFeedback;
                    break;
                case "memberChangePNumber":
                    changeMember.PersonalNumber = userFeedback;
                    break;
                case "memberLookAtPick":
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        if (number > 0)
                        {
                            
                            changeMember.UniqueId = this.memberList.getUniqueId(Int32.Parse(userFeedback));
                        }
                    }
                    break;
                default:
                    
                    break;
            }

            member = changeMember;
        }

        /*
        public string replaceMember(Member newMember)
        {
            string memberId = newMember.UniqueId;
            string line = "";
            string writeLine = "";
            bool membersFound = false;
            bool selectedMemberFound = false;
            try
            {   //Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("..\\..\\data\\registry.txt"))
                {
                    //Read the stream line by line
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Find the members
                        if (line == "#Members")
                        {
                            membersFound = true;
                            writeLine = writeLine + line + "@";
                            continue;
                        }
                        else if (line == "#")
                        {
                            membersFound = false;
                            writeLine = writeLine + line + "@";
                        }
                        else if (line == "##")
                        {
                            membersFound = false;
                            writeLine = writeLine + line + "@";
                        }
                        else
                        {

                            //Split the string and check member Id   
                            if (membersFound)
                            {
                                //Getting the properties of the member from the database (text file)
                                string[] stringSeparators = new string[] { ", " };
                                string[] result;

                                result = line.Split(stringSeparators, StringSplitOptions.None);

                                int counter = 1;
                                foreach (string s in result)
                                {
                                    if (counter == 1)
                                    {
                                        if (s == memberId)
                                        {
                                            selectedMemberFound = true;
                                        }
                                        writeLine = writeLine + s + ", ";
                                    }
                                    else if (counter == 2)
                                    {
                                        if (selectedMemberFound)
                                        {
                                            writeLine = writeLine + newMember.Name + ", ";
                                        }
                                        else
                                        {
                                            writeLine = writeLine + s + ", ";
                                        }
                                    }
                                    else if (counter == 3)
                                    {

                                        if (selectedMemberFound)
                                        {
                                            writeLine = writeLine + newMember.PersonalNumber + "@";
                                            selectedMemberFound = false;
                                        }
                                        else
                                        {
                                            writeLine = writeLine + s + "@";
                                        }
                                    }
                                    counter++;
                                }
                            }
                            else
                            {
                                writeLine = writeLine + line + "@";
                            }

                        }

                    }

                }
                return writeLine;
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return writeLine;
            }
        }
        */
        /*
        public string addMember(Member member)
        {
            // Put the entire registry into a string
            string registryText = File.ReadAllText("..\\..\\data\\registry.txt");

            //Get the first part of the registry
            string firstPartOfRegistry = registryText.Substring(0, 8);

            //Get the second part of the registry
            string secondPartOfRegistry = registryText.Substring(9);

            //Creating the new text to be added to the registry
            string newText = firstPartOfRegistry + "@" + member.UniqueId + ", " + member.Name + ", " + member.PersonalNumber + "@" + secondPartOfRegistry;

            return newText;

            
        }
        */
    }
}
