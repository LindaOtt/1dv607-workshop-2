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
            CreateMemberEnterName,
            CreateMemberEnterPNumber,
            NewMemberCreated,
            ChangeMemberPick,
            ChangeMemberEnterName,
            ChangeMemberEnterPNumber,
            ChangeMemberSaved,
            LookAtMemberPick,
            LookAtChosenMember,
            RegisterNewBoat,
            DeleteBoat,
            ChangeBoat,
            ChangeBoatEnterType,
            ChangeBoatEnterLength,
            WrongInput
        };

        //State variables
        private bool changeMemberEnterName = false;
        private bool changeMemberEnterPNumber = false;
        private bool changeMemberSaved = false;
        private bool lookAtMemberPick = false;
        private bool createNewMember = false;
        private bool createMemberEnterName = false;
        private bool createMemberEnterPNumber = false;

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
                    
                    writeToFile(writeLine);
                    //Console.WriteLine(writeLine);

                   
                    currentView = views.ChangeMemberSaved;
                    changeMemberSaved = false;
                }

                else if (lookAtMemberPick)
                {
                    currentView = views.LookAtMemberPick;
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        if (number > 0)
                        {
                            currentView = views.LookAtChosenMember;
                            changeMember.UniqueId = memberList.getUniqueId(Int32.Parse(userFeedback));
                        }
                        else
                        {
                            currentView = views.showFirstView;
                        }
                        lookAtMemberPick = false;
                    }
                }

                else if (createNewMember)
                {
                    changeMember.Name = userFeedback;
                    currentView = views.CreateMemberEnterName;
                    createNewMember = false;
                    createMemberEnterName = true;
                }

                else if (createMemberEnterName)
                {
                    changeMember.PersonalNumber = userFeedback;
                    currentView = views.CreateMemberEnterPNumber;
                    createMemberEnterName = false;
                    createMemberEnterPNumber = true;
                }
               
                else if (createMemberEnterPNumber)
                {
                    //Creating a unique id for the member
                    string uniqueId = generateId();
                    changeMember.UniqueId = uniqueId;

                    //Adding the member to the database
                    addMember(changeMember);

                    currentView = views.NewMemberCreated;
                    createMemberEnterPNumber = false;
                }

                //We are not changing a member or a boat,
                //or looking at a member
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
                        createNewMember = true;
                    }
                    else if (userFeedback == "D")
                    {
                        currentView = views.ChangeMemberPick;
                        changeMemberEnterName = true;
                    }
                    else if (userFeedback == "E")
                    {
                        currentView = views.LookAtMemberPick;
                        lookAtMemberPick = true;
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

        
        public void addMember(Member member)
        {
            // Open the file to read from.
            string registryText = File.ReadAllText("..\\..\\data\\registry.txt");

            //Get the first part of the registry
            string firstPartOfRegistry = registryText.Substring(0, 8);

            //Get the second part of the registry
            string secondPartOfRegistry = registryText.Substring(9);

            //Creating the new text to be added to the registry
            string newText = firstPartOfRegistry + "@" + member.UniqueId + ", " + member.Name + ", " + member.PersonalNumber + "@" + secondPartOfRegistry;

            //Adding the new text to the registry
            writeToFile(newText);
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
                case views.CreateMemberEnterName:
                    view.viewCreateMemberEnterName(changeMember);
                    break;
                case views.CreateMemberEnterPNumber:
                    view.viewCreateMemberEnterPNumber(changeMember);
                    break;
                case views.NewMemberCreated:
                    view.newMemberCreated(changeMember);
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
                case views.LookAtMemberPick:
                    view.viewLookAtMemberPick();
                    break;
                case views.LookAtChosenMember:
                    view.viewLookAtChosenMember(changeMember);
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

        public string generateId()
        {
            long currentTime = DateTime.Now.Ticks;
            string id = currentTime.ToString();
            return id;
        }
    }
}
