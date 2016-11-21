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
                case "boatChangeSetOrderNr":
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        Debug.WriteLine("Inside boatChangeSetOrderNr");
                        Debug.Write("userFeedback: ");
                        Debug.Write(userFeedback);
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
            }

            boat = changeBoat;
        }
        
    }
}
