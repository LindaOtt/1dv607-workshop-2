using System;
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
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\\data\\registry.txt");
            while ((line = file.ReadLine()) != null)
            {
                //System.Console.WriteLine(line);
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
