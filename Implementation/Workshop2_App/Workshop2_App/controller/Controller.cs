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
            ChangeMember,
            LookAtMember,
            RegisterNewBoat,
            DeleteBoat,
            ChangeBoat,
            WrongInput
        };

        public views currentView = views.showFirstView;

        public void showView()
        {

            View view = new View();
            
            //Prevent app from ending if ctrl+c is pressed
            //Console.TreatControlCAsInput = true;

            view.viewStart();

            string userFeedback;

            do {

                userFeedback = (Console.ReadLine()).ToUpper();

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
                        currentView = views.WrongInput;
                        break;
                }

                activateView();
                
            } while (userFeedback != "0");

        }

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
                case views.ChangeMember:
                    view.viewChangeMember();
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
