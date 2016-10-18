using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2_App.view;
using Workshop2_App.model;

namespace Workshop2_App.controller
{
    class Controller
    {

        //Getting the memberlist from MemberList class
        MemberList memberList = new MemberList();

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

        private Member changeMember = new Member();

        //private Boat changeBoat = new Boat();

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
                

                int number;
                if (changeMemberEnterName)
                {
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        if (number > 0)
                        {
                            currentView = views.ChangeMemberEnterName;
                            changeMember.UniqueId = memberList.getUniqueId(Int32.Parse(userFeedback));
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
                    changeMember.Name = userFeedback;
                    currentView = views.ChangeMemberEnterPNumber;
                    changeMemberEnterPNumber = false;
                    changeMemberSaved = true;
                }

                else if (changeMemberSaved)
                {
                    changeMember.PersonalNumber = userFeedback;
                    //Save member to textfile 
                    string writeLine = replaceMember(changeMember);
                    
                    System.Diagnostics.Debug.WriteLine("Debug: " + writeLine);
                    writeToFile(writeLine);
                    //Console.WriteLine(writeLine);

                   
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

        public void saveMember(Member member)
        {
            //Get the content of the text file
            string currentText = getInfoFromDb();
        }


        public string replaceMember(Member newMember)
        {
            string memberId = newMember.UniqueId;
            string line = "";
            string writeLine = "";
            bool membersFound = false;
            bool selectedMemberFound = false;
            try
            {   //Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("..\\..\\data\\registry.txt"))
                {
                    //Read the stream line by line
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Find the members
                        if (line == "#Members")
                        {
                            membersFound = true;
                            writeLine = writeLine + line + "@";
                            continue;
                        }
                        else if (line == "#")
                        {
                            membersFound = false;
                            writeLine = writeLine + line + "@";
                        }
                        else { 

                            //Split the string and check member Id   
                            if (membersFound)
                            {
                                //Getting the properties of the member from the database (text file)
                                string[] stringSeparators = new string[] { ", " };
                                string[] result;

                                result = line.Split(stringSeparators, StringSplitOptions.None);

                                int counter = 1;
                                foreach (string s in result)
                                {
                                    if (counter == 1) { 
                                        if (s == memberId)
                                        {
                                            selectedMemberFound = true;
                                        }
                                        writeLine = writeLine + s + ", ";
                                    }
                                    else if (counter == 2)
                                    {
                                        if (selectedMemberFound)
                                        {
                                            writeLine = writeLine + newMember.Name + ", ";
                                        }
                                        else
                                        {
                                            writeLine = writeLine + s + ", ";
                                        }
                                    }
                                    else if (counter == 3)
                                    {

                                        if (selectedMemberFound)
                                        {
                                            writeLine = writeLine + newMember.PersonalNumber + "@";
                                            selectedMemberFound = false;
                                        }
                                        else
                                        {
                                            writeLine = writeLine + s + "@";
                                        }
                                    }
                                    counter++;
                                }
                            }
                            else
                            {
                                writeLine = writeLine + line + "@";
                            }

                        }

                    }

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

        public String getInfoFromDb()
        {
            string line = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("..\\..\\registry.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    line = line + sr.ReadToEnd();
                    return line;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return line;
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
                    view.viewChangeMemberEnterName(changeMember);
                    break;
                case views.ChangeMemberEnterPNumber:
                    view.viewChangeMemberEnterPNumber(changeMember);
                    break;
                case views.ChangeMemberSaved:
                    view.viewChangeMemberSaved(changeMember);
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
            System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\data\\registry.txt", false);
            message = message.Replace("@", Environment.NewLine);
            file.WriteLine(message);

            file.Close();
        }
    }
}
