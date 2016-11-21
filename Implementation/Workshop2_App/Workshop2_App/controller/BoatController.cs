using System;
using System.IO;
using Workshop2_App.model;
using System.Diagnostics;

namespace Workshop2_App.controller
{
    class BoatController
    {
        private string inputFromUser;
        private string userFeedback;
        int number;
        private Boat boat;
        private BoatList boatList = new BoatList();

        public Boat getBoat()
        {
            return boat;
        }

        public void setBoatFromInput(string input, string userFeedback, Boat changeBoat)
        {
            switch (input)
            {
                case "boatChangeSetOrderNr":
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        if (number > 0)
                        {
                            changeBoat.OrderNumber = number;
                        }
                    }
                    break;
                case "boatChangeSetId":
                    changeBoat.UniqueId = userFeedback;
                    break;
                case "boatChangeSetLength":
                    changeBoat.Length = userFeedback;
                    break;
                case "boatChangeSetType":
                    changeBoat.Type = (Boat.type)Enum.Parse(typeof(Boat.type), Convert.ToString(int.Parse(userFeedback)-1));
                    break;
                case "boatDeletePick":
                    changeBoat.OrderNumber = number;
                    break;
                default:
                    Debug.WriteLine("inputFromUser: default");
                    break;
            } //End switch

            boat = changeBoat;
        } //End setBoatFromInput


        public string deleteBoatFromDb(int orderNumber)
        {
            string line = "";
            string writeLine = "";
            bool boatsFound = false;

            try
            {   //Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("..\\..\\data\\registry.txt"))
                {
                    int counter = 0;
                    //Read the stream line by line
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Find the members
                        if (line == "#Boats")
                        {
                            counter = 0;
                            boatsFound = true;
                            writeLine = writeLine + line + "@";
                        }
                        else if (line == "##")
                        {
                            boatsFound = false;
                            writeLine = writeLine + line + "@";
                        }
                        else
                        {
                            if (boatsFound)
                            {
                                if (counter != orderNumber)
                                {
                                    writeLine = writeLine + line + "@";
                                }

                            }
                            else
                            {
                                writeLine = writeLine + line + "@";
                            }

                        }//End else
                        counter++;
                    }//End while

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


        public string saveChangedBoat(Boat boat)
        {
            //Read the text file
            string line;
            string writeLine = "";
            int counter = 0;
            bool boatsFound = false;
            string boatOrderNumberString = (boat.OrderNumber).ToString();
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\\data\\registry.txt");
            while ((line = file.ReadLine()) != null)
            {

                //Getting the "Boats" part
                if (line == "#Boats")
                {
                    boatsFound = true;
                    counter = 0;
                    writeLine = writeLine + line;
                }
                else if (line == "##")
                {
                    boatsFound = false;
                    writeLine = writeLine + "@" + line;
                    break;
                }
                else
                {
                    if (boatsFound == true)
                    {
                        //This is the boat that needs to be replaced
                        if (counter == boat.OrderNumber)
                        {
                            writeLine = writeLine + "@" + boat.UniqueId + ", " + boat.Type + ", " + boat.Length;
                        }
                        else
                        {
                            writeLine = writeLine + "@" + line;
                        }

                    } //End if boatsfound
                    else
                    {
                        if (line == "#Members")
                        {
                            writeLine = writeLine + line;
                        }
                        else if (line == " ")
                        {
                            writeLine = writeLine + "@" + "@";
                        }
                        else if (line == "")
                        {
                            writeLine = writeLine + "@" + "@";
                        }
                        else
                        {
                            writeLine = writeLine + "@" + line;
                        }
                    }
                } //End else
                counter++;
            } //End while

            file.Close();
            Debug.Write("saveChangedBoat writeLine: ");
            Debug.Write(writeLine);
            Debug.WriteLine(" ");
            return writeLine;
        }
    }
    
}
