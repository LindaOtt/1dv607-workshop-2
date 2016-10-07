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
            ChangeBoat
        };

        public views currentView = views.showFirstView;

        public void showView()
        {
            
            View view = new View();

            ConsoleKeyInfo input;
            // Prevent example from ending if CTL+C is pressed.
            Console.TreatControlCAsInput = true;
            
            

            string userFeedback = "";
            do {

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
                    default:
                        currentView = views.showFirstView;
                        break;
                }

                //Checking to see which view should be shown
                switch (currentView)
                {
                    case views.showFirstView:
                        view.viewStart();
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
                    default:
                        view.viewStart();
                        break;
                }

                userFeedback = (Console.ReadLine()).ToUpper();
                
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
