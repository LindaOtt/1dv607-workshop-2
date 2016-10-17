﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop2_App.model
{
    
    class BoatList
    {
        //List to store the boats in
        private List<Boat> boats = new List<Boat>();


        //Gets the boats from the text file and puts them into the boat list
        public void getBoatsFromDb()
        {
            //Read the text file
            string line;
            Boolean boatsFound = false;
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\\data\\registry.txt");
            while ((line = file.ReadLine()) != null)
            {
                //System.Console.WriteLine(line);
                //Getting the "Members" part
                if (line=="#Members")
                {
                    boatsFound = true;
                }
                else if (line=="#")
                {
                    boatsFound = false;
                    break;
                }
                else
                {
                    if (boatsFound)
                    {
                        //Creating a new Member and adding it to the list
                        Boat boat = new Boat();

                        //Getting the name of the member
                        string[] stringSeparators = new string[] { ", " };
                        string[] result;

                        result = line.Split(stringSeparators, StringSplitOptions.None);

                        int counter = 1;
                        foreach (string s in result)
                        {

                            if (counter == 1) { 
                                //Adding the type to the boat
                                //boat.Type = s;
                            }
                            else if (counter == 2)
                            {
                                //Adding the length to the boat
                                //boat.Length = s;
                            }
                            counter++;
                        }

                        //Adding the member to the list
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