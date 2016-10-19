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
            DeleteBoatPick,
            DeletedBoat,
            ChangeBoat,
            ChangeBoatEnterId,
            ChangeBoatEnterType,
            ChangeBoatEnterLength,
            registerBoatEnterType,
            registerBoatEnterLength,
            registerBoatSaved,
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
        private bool registerNewBoat = false;
        private bool registerBoatEnterType = false;
        private bool registerBoatEnterLength = false;
        private bool deleteBoat = false;
        private bool changeBoatPick = false;
        private bool changeBoatEnterId = false;
        private bool changeBoatEnterType = false;
        private bool changeBoatEnterLength = false;
        private Member changeMember = new Member();
        private Boat changeBoat = new Boat();

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

                else if (registerNewBoat)
                {
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        if (number > 0)
                        {
                            currentView = views.registerBoatEnterType;
                            changeMember.UniqueId = memberList.getUniqueId(Int32.Parse(userFeedback));
                        }
                        else
                        {
                            currentView = views.showFirstView;
                        }
                        registerNewBoat = false;
                        registerBoatEnterType = true;
                    }

                }

                else if (registerBoatEnterType)
                {
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        if (number > 0)
                        {
                            number--;
                            currentView = views.registerBoatEnterLength;

                            Boat.type boatType = ((Boat.type)number);

                            changeBoat.Type = boatType;

                        }
                        else
                        {
                            currentView = views.showFirstView;
                        }
                        registerBoatEnterType = false;
                        registerBoatEnterLength = true;
                    }
                }

                else if (registerBoatEnterLength)
                {
                    changeBoat.Length = userFeedback;
                    registerBoatEnterLength = false;
                    registerBoatForUser(changeMember, changeBoat);
                    currentView = views.registerBoatSaved;
                }

                else if (deleteBoat)
                {
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        if (number > 0)
                        {
                            currentView = views.DeleteBoatPick;
                            changeBoat.OrderNumber = number;
                            string newBoatsText = deleteBoatFromDb(number);
                            writeToFile(newBoatsText);
                        }
                        else
                        {
                            currentView = views.showFirstView;
                        }
                        deleteBoat = false;
                    }
                }

                else if (changeBoatPick)
                {
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        if (number > 0)
                        {
                            currentView = views.ChangeBoatEnterId;
                            changeBoat.OrderNumber = number;
                        }
                        else
                        {
                            currentView = views.showFirstView;
                        }
                        changeBoatPick = false;
                        changeBoatEnterId = true;
                    }
                }

                else if (changeBoatEnterId)
                {
                    currentView = views.ChangeBoatEnterId;
                    changeBoatEnterId = false;
                    changeBoatEnterType = true;
                }
                //We are not changing a member or a boat,
                //or looking at a member
                //or registering a new boat for a member
                //so show the regular menu options
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
                        registerNewBoat = true;
                    }
                    else if (userFeedback == "G")
                    {
                        currentView = views.DeleteBoat;
                        deleteBoat = true;
                    }
                    else if (userFeedback == "H")
                    {
                        currentView = views.ChangeBoat;
                        changeBoatPick = true;
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
            // Put the entire registry into a string
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

        public void registerBoatForUser(Member member, Boat boat)
        {
            // Put the entire registry into a string
            string registryText = File.ReadAllText("..\\..\\data\\registry.txt");

            //Find the boats part in the registry
            int boatsIndex = registryText.IndexOf("#Boats");

            //Get the first part of the registry
            string firstPartOfRegistry = registryText.Substring(0, boatsIndex);

            //Get the second part of the registry
            string secondPartOfRegistry = registryText.Substring(boatsIndex);

            //Get the third part of the registry
            string thirdPartOfRegistry = secondPartOfRegistry.Substring(6);

            //Get the second part of the registry
            secondPartOfRegistry = secondPartOfRegistry.Substring(0,6);

            //Creating the new text to be added to the registry
            string newText = firstPartOfRegistry + secondPartOfRegistry + "@" + member.UniqueId + ", " + boat.Type + ", " + boat.Length  + thirdPartOfRegistry;

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

        public string getInfoFromDb()
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
                case views.DeleteBoatPick:
                    view.viewDeleteBoatPick(changeBoat);
                    break;
                case views.ChangeBoat:
                    view.viewChangeBoat();
                    break;
                case views.registerBoatEnterType:
                    view.registerBoatEnterType(changeMember, changeBoat);
                    break;
                case views.registerBoatEnterLength:
                    view.registerBoatEnterLength(changeMember, changeBoat);
                    break;
                case views.registerBoatSaved:
                    view.registerBoatSaved(changeMember, changeBoat);
                    break;
                case views.ChangeBoatEnterId:
                    view.changeBoatEnterId(changeBoat);
                    break;
                case views.WrongInput:
                    view.viewWrongInput();
                    break;
                default:
                    break;
            }

        }

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


        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
