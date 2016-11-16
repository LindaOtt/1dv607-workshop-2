using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2_App.view;
using Workshop2_App.model;

namespace Workshop2_App.controller
{
    class BoatController
    {
        private string inputFromUser;

        private View view;

        private Boat changeBoat;

        public Boat getBoat(string input, string userFeedback)
        {
            inputFromUser = input;
            return changeBoat;
        }

        public View getView(string input)
        {
            inputFromUser = input;
            return view;
        }
    }
}
