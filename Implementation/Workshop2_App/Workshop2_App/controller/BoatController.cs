using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2_App.view;
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
                case "boatChangeSetType":
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        if (number > 0)
                        {
                            number--;

                            Boat.type boatType = ((Boat.type)number);

                            changeBoat.Type = boatType;

                        }
                    }
                    break;

                case "boatChangeSetLength":
                    changeBoat.Length = userFeedback;
                    break;

                case "boatDeletePick":
                    
                    changeBoat.OrderNumber = number;
                    break;


                default:
                    Debug.WriteLine("inputFromUser: default");
                    break;
            }

            boat = changeBoat;
        }
        
    }
}
