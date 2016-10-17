using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2_App.view;

namespace Workshop2_App.controller
{
    class Controller
    {

        public enum views
        {
            showFirstView,
            ListAllCompact,
            ListAllVerbose,
            CreateNewMember,
            ChangeMemberPick,
            ChangeMemberEnterData,
            LookAtMember,
            RegisterNewBoat,
            DeleteBoat,
            ChangeBoat,
            WrongInput
        };

        private int changeMemberId;

        private int changeBoatId;

        public views currentView = views.showFirstView;

        public void showView()
        {

            View view = new View();
            
            //Prevent app from ending if ctrl+c is pressed
            //Console.TreatControlCAsInput = true;

            view.viewStart();

            string userFeedback;
            int number;

            do {

                userFeedback = (Console.ReadLine()).ToUpper();
                

                if (userFeedback == "A")
                {
                    currentView = views.ListAllCompact;
                }
                else if (userFeedback == "B")
                {
                    currentView = views.ListAllVerbose;
                }
                else if (userFeedback == "C")
                {
                    currentView = views.CreateNewMember;
                }
                else if (userFeedback == "D")
                {
                    currentView = views.ChangeMemberPick;
                }
                else if (userFeedback == "E")
                {
                    currentView = views.LookAtMember;
                }
                else if (userFeedback == "F")
                {
                    currentView = views.RegisterNewBoat;
                }
                else if (userFeedback == "G")
                {
                    currentView = views.DeleteBoat;
                }
                else if (userFeedback == "H")
                {
                    currentView = views.ChangeBoat;
                }
                else if (userFeedback == "0")
                {
                    currentView = views.showFirstView;
                }
                else if (Int32.TryParse(userFeedback, out number))
                {
                    if(number > 0)
                    {
                        currentView = views.ChangeMemberEnterData;
                    }
                    else
                    {
                        currentView = views.showFirstView;
                    }
                }
                else
                {
                    currentView = views.showFirstView;
                }

                /*
                switch (userFeedback)
                {
                    case "A":
                        currentView = views.ListAllCompact;
                        break;
                    case "B":
                        currentView = views.ListAllVerbose;
                        break;
                    case "C":
                        currentView = views.CreateNewMember;
                        break;
                    case "D":
                        currentView = views.ChangeMember;
                        break;
                    case "E":
                        currentView = views.LookAtMember;
                        break;
                    case "F":
                        currentView = views.RegisterNewBoat;
                        break;
                    case "G":
                        currentView = views.DeleteBoat;
                        break;
                    case "H":
                        currentView = views.ChangeBoat;
                        break;
                    case "0":
                        currentView = views.showFirstView;
                        break;
                    default:
                        //If the user entered a number higher than 0
                        if (checkInput(userFeedback))
                        {
                            currentView = views.
                        }
                        else { 
                            currentView = views.WrongInput;
                        }
                        break;
                        }

                }
                */

                activateView();
                
            } while (userFeedback != "0");

        }

        /*
        public bool checkInput(string userFeedback)
        {
            int number;
            bool isValidInt = false;

            bool isInt = Int32.TryParse(userFeedback, out number);
            if (isInt)
            {
                int userFeedbackInt = Int32.Parse(userFeedback);
                if (userFeedbackInt > 0) { 
                    isValidInt = true;
                    return isValidInt;
                }
            }
            return isValidInt;
        }
        */

        public void getInfoFromFile()
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("..\\..\\registry.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public void activateView()
        {

            View view = new View();
            view.viewStart();

            //Checking to see which view should be shown
            switch (currentView)
            {
                case views.showFirstView:
                    break;
                case views.ListAllCompact:
                    view.viewListAllCompact();
                    break;
                case views.ListAllVerbose:
                    view.viewListAllVerbose();
                    break;
                case views.CreateNewMember:
                    view.viewCreateNewMember();
                    break;
                case views.ChangeMemberPick:
                    view.viewChangeMemberPick();
                    break;
                case views.LookAtMember:
                    view.viewLookAtMember();
                    break;
                case views.RegisterNewBoat:
                    view.viewRegisterNewBoat();
                    break;
                case views.DeleteBoat:
                    view.viewDeleteBoat();
                    break;
                case views.ChangeBoat:
                    view.viewChangeBoat();
                    break;
                case views.WrongInput:
                    view.viewWrongInput();
                    break;
                default:
                    break;
            }

            /*
            //Checking to see which view should be shown
            switch (currentView)
            {
                case views.showFirstView:
                    view.viewStart();
                    break;
                case views.ListAllCompact:
                    view.viewStart();
                    view.viewListAllCompact();
                    break;
                case views.ListAllVerbose:
                    view.viewStart();
                    view.viewListAllVerbose();
                    break;
                case views.CreateNewMember:
                    view.viewStart();
                    view.viewCreateNewMember();
                    break;
                case views.ChangeMember:
                    view.viewStart();
                    view.viewChangeMember();
                    break;
                case views.LookAtMember:
                    view.viewStart();
                    view.viewLookAtMember();
                    break;
                case views.RegisterNewBoat:
                    view.viewStart();
                    view.viewRegisterNewBoat();
                    break;
                case views.DeleteBoat:
                    view.viewStart();
                    view.viewDeleteBoat();
                    break;
                case views.ChangeBoat:
                    view.viewStart();
                    view.viewChangeBoat();
                    break;
                case views.WrongInput:
                    view.viewStart();
                    view.viewWrongInput();
                    break;
                default:
                    view.viewStart();
                    break;
            }
            */

        }

        public void writeToFile(string message)
        {
            // Write the string to a file.
            //System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Users\\Fam\\Google Drive\\Webbprogrammerare\\1DV607 Objektorienterad analys och design med UML\\Workshop II\\Implementation\\Workshop2_App\\Workshop2_App\\registry.txt", false);
            System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\registry.txt", false);
            file.WriteLine(message);

            file.Close();
        }
    }
}
