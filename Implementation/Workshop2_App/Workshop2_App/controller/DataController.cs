using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Workshop2_App.model;

namespace Workshop2_App.controller
{
    class DataController
    {
        private string registryText = File.ReadAllText("..\\..\\data\\registry.txt");

        private string newText = "";

        public string changeMemberRegistry(Member member, string action)
        {

            if (action == "add") { 
                //Get the first part of the registry
                string firstPartOfRegistry = registryText.Substring(0, 8);

                //Get the second part of the registry
                string secondPartOfRegistry = registryText.Substring(9);

                //Creating the new text to be added to the registry
                newText = firstPartOfRegistry + "@" + member.UniqueId + ", " + member.Name + ", " + member.PersonalNumber + "@" + secondPartOfRegistry;

                return newText;
            }

            if (action == "change")
            {
                //Find where the chosen member line starts
                int memberIndexInWholeText = registryText.IndexOf(member.UniqueId);

                //Get the part of the registry before the chosen member
                string beforeMemberPart = registryText.Substring(0, memberIndexInWholeText);

                //Get the part of the registry that starts with the member id
                string memberPartIncludingMember = registryText.Substring(memberIndexInWholeText);

                //Get index of where the line of the chosen member ends
                int endOfChosenMemberIndex = memberPartIncludingMember.IndexOf(Environment.NewLine);

                //Get the part of the registry after the chosen member
                string afterMemberPart = memberPartIncludingMember = memberPartIncludingMember.Substring(endOfChosenMemberIndex);

                //Building a new registry text
                string newText = beforeMemberPart + member.UniqueId + ", " + member.Name + ", " + member.PersonalNumber + afterMemberPart;

                return newText;
            } 

            return newText;
            
        }

        public string addBoatToMemberInRegistry(Member member, Boat boat)
        {
            //Index of the boat part in the registry
            int boatIndex = registryText.IndexOf("#Boats");

            //Getting the part before the boat registry
            string partOfRegistryBeforeBoats = registryText.Substring(0, boatIndex);

            //Getting the boat registry, including title "#Boats"
            string partOfRegistryBoatsInclTitle = registryText.Substring(boatIndex);

            //Finding the position of where the "#Boats" line ends
            int endOfBoatsTitle = partOfRegistryBoatsInclTitle.IndexOf(Environment.NewLine);

            //Boat title only
            string boatTitle = partOfRegistryBoatsInclTitle.Substring(0, endOfBoatsTitle);

            //Getting the part before the boats
            string firstPartOfRegistry = registryText.Substring(boatIndex);

            //Finding the position of the start of the boats registry
            //Get index of where the line of the chosen member ends 
            int startOfBoatsIndex = firstPartOfRegistry.IndexOf(Environment.NewLine);

            //Getting the boats only 
            string boatRegistry = firstPartOfRegistry.Substring(startOfBoatsIndex);

            //Putting the boats registry together
            newText = partOfRegistryBeforeBoats + boatTitle + "@" + member.UniqueId + ", " + boat.Type + ", " + boat.Length + boatRegistry;

            return newText;
        }

        public string changeBoatRegistry(Boat boat, string action)
        {
            //Find index of where the boat registry starts
            int boatIndex = registryText.IndexOf("#Boats");

            string registryUpUntilBoats = registryText.Substring(0, boatIndex);

            //Get the first part of the registry before "#Boats"
            string firstPartOfRegistry = registryText.Substring(boatIndex);

            //Finding the position of where the "#Boats" line ends
            int endOfBoatsTitle = firstPartOfRegistry.IndexOf(Environment.NewLine);

            //Boat title only
            string boatTitle = firstPartOfRegistry.Substring(0, endOfBoatsTitle);

            //Find the end of the line with the boat title
            int boatTitleStopIndex = firstPartOfRegistry.IndexOf(Environment.NewLine);

            //Find the boat registry part only
            string boatRegistryOnly = firstPartOfRegistry.Substring(boatTitleStopIndex);

            string[] boatList = boatRegistryOnly.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int counter = 1;

            newText = registryUpUntilBoats + boatTitle + "@";

            foreach (string line in boatList)
            {
                    
                if (line == "##")
                {
                    newText = newText + line;
                }
                else { 
                    if (counter == boat.OrderNumber)
                    {
                        if (action == "change")
                        {
                            newText = newText + boat.UniqueId + ", " + boat.Type + ", " + boat.Length + "@";
                        }
                        else if (action == "delete")
                        {
                            //do nothing
                        }
                    }
                    else
                    {
                        newText = newText + line + "@";
                    }
                }
                counter++;
            }

            Console.WriteLine(newText);
            return newText;
        }

        public void writeToFile(string message)
        {
            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\data\\registry.txt", false);
            message = message.Replace("@", Environment.NewLine);
            file.WriteLine(message);

            file.Close();
        }
    }
}


    
