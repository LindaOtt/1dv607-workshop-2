using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop2_App.model
{
    
    class BoatList
    {
        //List to store the boats in
        private List<Boat> boats = new List<Boat>();

        //Member Id to retrieve boats for
        private string uniqueId;

        //Creates the boat list for the member
        public BoatList(string memberId=null)
        {
            uniqueId = memberId;
            getBoatsFromDb();
        }


        //Gets the boats from the text file and puts them into the boat list
        public void getBoatsFromDb()
        {
            
            //Read the text file
            string line;
            bool boatsFound = false;
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\\data\\registry.txt");
            while ((line = file.ReadLine()) != null)
            {
                
                //Getting the "Boats" part
                if (line=="#Boats")
                {
                    boatsFound = true;
                }
                else if (line=="##")
                {
                    boatsFound = false;
                    break;
                }
                else
                {
                    if (boatsFound == true)
                    {
                        //Creating a new Boat 
                        Boat boat = new Boat();

                        //Getting the data of the boat
                        string[] stringSeparators = new string[] { ", " };
                        string[] result;

                        result = line.Split(stringSeparators, StringSplitOptions.None);

                        int counter = 1;
                        bool belongsToMember = false;
                        foreach (string s in result)
                        {
                            /** Checking to see if the member id of the boat
                            ¨   corresponds to the uniqueId
                            **/
                            if (counter == 1)
                            {
                                if (s == uniqueId)
                                {
                                    belongsToMember = true;
                                }
                            }
                            if (counter == 2 && belongsToMember) { 
                                //Adding the type to the boat
                                boat.Type = (Boat.type) Enum.Parse(typeof(Boat.type), s);
                            }
                            else if (counter == 3 && belongsToMember)
                            {
                                //Adding the length to the boat
                                boat.Length = s;
                            }
                            counter++;
                        }

                        //Adding the member to the list
                        if (belongsToMember) { 
                            boats.Add(boat);
                        }
                    }
                }
            }

            file.Close();
        }

        public void getAllBoatsFromDb()
        {
            //Read the text file
            string line;
            bool boatsFound = false;
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\\data\\registry.txt");
            while ((line = file.ReadLine()) != null)
            {

                //Getting the "Boats" part
                if (line == "#Boats")
                {
                    boatsFound = true;
                }
                else if (line == "##")
                {
                    boatsFound = false;
                    break;
                }
                else
                {
                    if (boatsFound == true)
                    {
                        //Creating a new Boat 
                        Boat boat = new Boat();

                        //Getting the data of the boat
                        string[] stringSeparators = new string[] { ", " };
                        string[] result;

                        result = line.Split(stringSeparators, StringSplitOptions.None);

                        int counter = 1;
                        foreach (string s in result)
                        {
                            /** Checking to see if the member id of the boat
                            ¨   corresponds to the uniqueId
                            **/
                            if (counter == 1)
                            {
                                boat.UniqueId = s;
                            }
                            if (counter == 2)
                            {
                                //Adding the type to the boat
                                boat.Type = (Boat.type)Enum.Parse(typeof(Boat.type), s);
                            }
                            else if (counter == 3)
                            {
                                //Adding the length to the boat
                                boat.Length = s;
                            }
                            counter++;
                        } //End foreach

                        //Adding the boat to the list
                        boats.Add(boat);
                        } 
                    } 
                } 
            

            file.Close();
        }

        //Returns the list of boats
        public List<Boat> getBoatList()
        {
            return boats;
        }
    }
}
