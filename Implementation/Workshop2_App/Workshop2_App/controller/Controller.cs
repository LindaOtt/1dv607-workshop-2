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
            ChangeMemberEnterName,
            ChangeMemberEnterPNumber,
            ChangeMemberSaved,
            LookAtMember,
            RegisterNewBoat,
            DeleteBoat,
            ChangeBoat,
            ChangeBoatEnterType,
            ChangeBoatEnterLength,
            WrongInput
        };

        private bool changeMemberEnterName = false;
        private bool changeMemberEnterPNumber = false;
        private bool changeMemberSaved = false;

        private int changeMemberId;

        private bool changeBoat = false;

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
                
                //Check if we are currently changing a member 
                if (changeMemberEnterName)
                {
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        if (number > 0)
                        {
                            currentView = views.ChangeMemberEnterName;
                            changeMemberId = number;
                        }
                        else
                        {
                            currentView = views.showFirstView;
                        }
                    }
                    changeMemberEnterName = false;
                    changeMemberEnterPNumber = true;
                }

                else if (changeMemberEnterPNumber)
                {
                    currentView = views.ChangeMemberEnterPNumber;
                    changeMemberEnterPNumber = false;
                    changeMemberSaved = true;
                }

                else if (changeMemberSaved)
                {
                    currentView = views.ChangeMemberSaved;
                    changeMemberSaved = false;
                }

                //We are not changing a member or a boat,
                //show the regular menu options
                else { 
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
                        changeMemberEnterName = true;
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
               
                    else
                    {
                        currentView = views.showFirstView;
                    }
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
                case views.ChangeMemberPick:
                    view.viewChangeMemberPick();
                    break;
                case views.ChangeMemberEnterName:
                    view.viewChangeMemberEnterName(changeMemberId);
                    break;
                case views.ChangeMemberEnterPNumber:
                    view.viewChangeMemberEnterPNumber(changeMemberId);
                    break;
                case views.ChangeMemberSaved:
                    view.viewChangeMemberSaved(changeMemberId);
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
