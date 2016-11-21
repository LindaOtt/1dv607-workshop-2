using System;
using System.IO;
using Workshop2_App.view;
using Workshop2_App.model;
using System.Diagnostics;

namespace Workshop2_App.controller
{
    class MasterController
    {

        private View view;

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
            DeleteBoatSave,
            DeleteBoatPick,
            DeletedBoat,
            ChangeBoat,
            ChangeBoatEnterId,
            ChangeBoatEnterType,
            ChangeBoatEnterLength,
            ChangeBoatSaved,
            registerBoatEnterType,
            registerBoatEnterLength,
            registerBoatSaved,
            WrongInput
        };
        
        //New state variables
        private bool memberControl = false; //Shows if we are working on members
        private bool boatControl = false; //Shows if we are working on boats
        private bool createControl = false; //Shows if we are creating something
        private bool changeControl = false; //Shows if we are changing something
        private bool deleteControl = false; //Shows if we are deleting something
        private bool lookControl = false; //Shows if we are looking at something
        private bool pickControl = false; //Shows if we are picking something from a list
        private bool enterNameControl = false; //Shows if we are entering a name
        private bool enterPNumberControl = false; //Shows if we are entering a personal number
        private bool typeControl = false; //Shows that we are entering the type of something
        private bool lengthControl = false; //Shows that we are entering the length of something
        private bool idControl = false; //Shows that we are entering the id of something

        private bool showByeMsg = false;

        private Member changeMember = new Member();
        private Boat changeBoat = new Boat();

        private BoatController boatController = new BoatController();
        private MemberController memberController;

        public views currentView = views.showFirstView;

        public MasterController(View sentView)
        {
            view = sentView;
            memberController = new MemberController();
        }

        public void showView()
        {

            view.viewStart();

            string userFeedback;

            do {

                userFeedback = (Console.ReadLine()).ToUpper();
                
                string input = "";
                
                //We are working on a member
                if (memberControl == true)
                {
                    Debug.WriteLine("Inside memberControl");
                    //We are creating a new member
                    if (createControl == true)
                    {
                        //We are entering the name for a new member
                        if (enterNameControl == true)
                        {
                            
                            input = "memberEnterName";
                            memberController.setMemberFromInput(input, userFeedback, changeMember);
                            changeMember = memberController.getMember();
                            enterNameControl = false;
                            enterPNumberControl = true;
                            currentView = views.CreateMemberEnterPNumber;
                        }

                        //We are entering the personal number for a new member
                        else if (enterPNumberControl == true)
                        {
                            
                            input = "memberEnterPNumber";
                            
                            memberController.setMemberFromInput(input, userFeedback, changeMember);
                            changeMember = memberController.getMember();

                            //Adding the unique id
                            input = "memberCreateSave";

                            //Creating a unique id for the member
                            string uniqueId = generateId();
                            memberController.setMemberFromInput(input, uniqueId, changeMember);

                            enterPNumberControl = false;

                            //Adding the member to the database
                            string newText = memberController.addMember(changeMember);
                            
                            //Adding the new text to the registry
                            writeToFile(newText);

                            memberControl = false;
                            createControl = false;

                            currentView = views.NewMemberCreated;
                        }
                    }
                    //We are changing a member
                    else if (changeControl == true)
                    {
                        //We are picking the member to change
                        if (pickControl == true)
                        {
                            input = "memberChangeSetId";
                            memberController.setMemberFromInput(input, userFeedback, changeMember);
                            changeMember = memberController.getMember();
                            pickControl = false;
                            enterNameControl = true;
                            currentView = views.ChangeMemberEnterName;
                        }
                        //We are changing the name for a member
                        else if (enterNameControl == true)
                        {
                            input = "memberChangeName";
                            memberController.setMemberFromInput(input, userFeedback, changeMember);
                            //changeMember = memberController.getMember();
                            enterNameControl = false;
                            enterPNumberControl = true;
                            currentView = views.ChangeMemberEnterPNumber;
                        }
                        //We are changing the personal number for a member
                        else if (enterPNumberControl == true)
                        {
                            Debug.WriteLine("Inside enterPNumberControl");
                            input = "memberChangePNumber";
                            memberController.setMemberFromInput(input, userFeedback, changeMember);
                            changeMember = memberController.getMember();

                            //Checking that member is ok
                            Debug.WriteLine(changeMember.Name);
                            Debug.WriteLine(changeMember.PersonalNumber);
                            Debug.WriteLine(changeMember.UniqueId);

                            //Save member to textfile 
                            string writeLine = memberController.replaceMember(changeMember);

                            writeToFile(writeLine);
                            memberControl = false;
                            changeControl = false;

                            enterPNumberControl = false;
                            currentView = views.ChangeMemberSaved;
                        }
                    }
                    //We are looking at a member
                    else if (lookControl == true)
                    {
                        //We are picking a member to look at
                        if (pickControl == true)
                        {
                            Debug.WriteLine("Inside pickControl");
                            input = "memberChangeSetId";
                            
                            memberController.setMemberFromInput(input, userFeedback, changeMember);
                            changeMember = memberController.getMember();

                            currentView = views.LookAtChosenMember;

                            memberControl = false;
                            lookControl = false;
                            pickControl = false;
                        }

                       
                    }
                    
                    Debug.WriteLine("changeMember name: {0}", changeMember.Name);
                }
                else if (boatControl == true)
                {
                    //We are creating a new boat for a member
                    if (createControl == true)
                    {
                        if (pickControl == true)
                        {
                            input = "memberChangeSetId";
                            memberController.setMemberFromInput(input, userFeedback, changeMember);
                            changeMember = memberController.getMember();

                            currentView = views.registerBoatEnterType;

                            pickControl = false;
                            typeControl = true;
                        }


                        else if (typeControl == true)
                        {
                            Debug.WriteLine("Inside typeControl");
                            input = "boatChangeSetType";
                            boatController.setBoatFromInput(input, userFeedback, changeBoat);
                            changeBoat = boatController.getBoat();

                            currentView = views.registerBoatEnterLength;

                            typeControl = false;
                            lengthControl = true;
                        }

                        else if (lengthControl == true)
                        {
                            input = "boatChangeSetLength";
                            boatController.setBoatFromInput(input, userFeedback, changeBoat);

                            changeBoat = boatController.getBoat();

                            registerBoatForUser(changeMember, changeBoat);

                            currentView = views.registerBoatSaved;

                            boatControl = false;
                            lengthControl = false;
                        }
                    }
                    //We are deleting a boat
                    else if (deleteControl == true)
                    {

                        if (pickControl == true)
                        {
                            int number;
                            if (Int32.TryParse(userFeedback, out number))
                            {
                                if (number > 0)
                                {
                                    input = "boatDeletePick";

                                    boatController.setBoatFromInput(input, userFeedback, changeBoat);

                                    changeBoat = boatController.getBoat();

                                    changeBoat.OrderNumber = number;
                                    string newBoatsText = boatController.deleteBoatFromDb(number);

                                    writeToFile(newBoatsText);
                                    currentView = views.DeleteBoatSave;
                                }
                            }
                            boatControl = false;
                            pickControl = false;
                        }

                        deleteControl = false;
                    }
                    //We are changing a boat
                    else if (changeControl == true)
                    {
                        if (pickControl == true)
                        {
                            input = "boatChangeSetOrderNr";
                            //Setting order number for the boat
                            boatController.setBoatFromInput(input, userFeedback, changeBoat);

                            changeBoat = boatController.getBoat();
                            Debug.Write("changeBoat: ");
                            Debug.Write(changeBoat.OrderNumber);
                            Debug.WriteLine("");
                            currentView = views.ChangeBoatEnterId;
                            pickControl = false;
                            idControl = true;

                        }


                        else if (idControl == true)
                        {
                            input = "boatChangeSetId";

                            boatController.setBoatFromInput(input, userFeedback, changeBoat);

                            changeBoat = boatController.getBoat();

                            currentView = views.ChangeBoatEnterType;
                            idControl = false;
                            typeControl = true;
                        }

                        else if (typeControl == true)
                        {
                            input = "boatChangeSetType";
                            //Setting type for the boat
                            boatController.setBoatFromInput(input, userFeedback, changeBoat);
                            changeBoat = boatController.getBoat();

                            currentView = views.ChangeBoatEnterLength;
                            typeControl = false;
                            lengthControl = true;
                        }
                        else if (lengthControl == true)
                        {
                            input = "boatChangeSetLength";

                            //Setting length for the boat
                            boatController.setBoatFromInput(input, userFeedback, changeBoat);
                            changeBoat = boatController.getBoat();

                            //Saving the boat
                            //Updating the registry text with the updated boat
                            string saveBoatText = boatController.saveChangedBoat(changeBoat);

                            //Saving the boat to the registry
                            writeToFile(saveBoatText);

                            currentView = views.ChangeBoatSaved;

                            boatControl = false;
                            changeControl = false;
                            lengthControl = false;
                            
                        }
                    }
                    Debug.WriteLine("input: {0}", input);
                }
                else {

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
                            memberControl = true;
                            createControl = true;
                            enterNameControl = true;
                            break;
                        case "D":
                            currentView = views.ChangeMemberPick;
                            memberControl = true;
                            changeControl = true;
                            pickControl = true;
                            break;
                        case "E":
                            currentView = views.LookAtMemberPick;
                            memberControl = true;
                            lookControl = true;
                            pickControl = true;
                            break;
                        case "F":
                            currentView = views.RegisterNewBoat;
                            boatControl = true;
                            createControl = true;
                            pickControl = true;
                            break;
                        case "G":
                            currentView = views.DeleteBoatPick;
                            boatControl = true;
                            deleteControl = true;
                            pickControl = true;
                            break;
                        case "H":
                            currentView = views.ChangeBoat;
                            boatControl = true;
                            changeControl = true;
                            pickControl = true;
                            break;
                        case "0":
                            currentView = views.showFirstView;
                            break;
                        default:
                            currentView = views.showFirstView;
                            break;
                    }
                    
                }

                if (userFeedback == "0")
                {
                    showByeMsg = true;
                }
                activateView(showByeMsg);
                Debug.WriteLine("ActivateView() run");
                Debug.WriteLine(" ");
                
            } while (userFeedback != "0");

        }

        /*
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
        */

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
        
        /*
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
                        else if (line == "##")
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
        */
        /*
        public string saveChangedBoat(Boat boat)
        {
            //Read the text file
            string line;
            string writeLine = "";
            int counter = 0;
            bool boatsFound = false;
            string boatOrderNumberString = (boat.OrderNumber).ToString();
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\\data\\registry.txt");
            while ((line = file.ReadLine()) != null)
            {

                //Getting the "Boats" part
                if (line == "#Boats")
                {
                    boatsFound = true;
                    counter = 0;
                    writeLine = writeLine + line;
                }
                else if (line == "##")
                {
                    boatsFound = false;
                    writeLine = writeLine + "@" + line;
                    break;
                }
                else
                {
                    if (boatsFound == true)
                    {
                        Debug.WriteLine(" ");
                        Debug.Write("Boat ordernumber: ");
                        Debug.Write(boat.OrderNumber);
                        Debug.WriteLine(" ");

                        Debug.WriteLine(" ");
                        Debug.Write("Counter: ");
                        Debug.Write(counter);
                        Debug.WriteLine(" ");
                        //This is the boat that needs to be replaced
                        if (counter == boat.OrderNumber) {
                            Debug.WriteLine("Counter equals boat.OrderNumber");
                            writeLine = writeLine + "@" + boat.UniqueId + ", " + boat.Type + ", " + boat.Length;
                        }
                        else
                        {
                            writeLine = writeLine + "@" + line;
                        }

                    } //End if boatsfound
                    else
                    {
                        if (line == "#Members")
                        {
                            writeLine = writeLine + line;
                        }
                        else if (line == " ")
                        {
                            writeLine = writeLine + "@" + "@";
                        }
                        else if (line == "")
                        {
                            writeLine = writeLine + "@" + "@";
                        }
                        else { 
                            writeLine = writeLine + "@" + line;
                        }
                    }
                } //End else
                counter++;
            } //End while

            file.Close();
            Debug.Write("saveChangedBoat writeLine: ");
            Debug.Write(writeLine);
            Debug.WriteLine(" ");
            return writeLine;
        }
        */

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

        public void activateView(bool showByeMsg)
        {

            View view = new View();
            if (showByeMsg)
            {
                view.viewBye();
            }
            else
            {
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
                    case views.CreateMemberEnterPNumber:
                        Debug.WriteLine(" ");
                        Debug.WriteLine("Case views.CreateMemberEnterPNumber");
                        Debug.WriteLine(" ");
                        view.viewCreateMemberEnterPNumber(changeMember);
                        break;
                    case views.NewMemberCreated:
                        view.newMemberCreated(changeMember);
                        break;
                    case views.ChangeMemberPick:
                        view.viewChangeMemberPick();
                        break;
                    case views.ChangeMemberEnterName:
                        Debug.WriteLine(" ");
                        Debug.WriteLine("Case views.ChangeMemberEnterName");
                        Debug.WriteLine(" ");
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
                    case views.DeleteBoatPick:
                        view.viewDeleteBoatPick();
                        break;
                    case views.DeleteBoatSave:
                        view.viewDeleteBoatSave(changeBoat);
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
                    case views.ChangeBoatEnterType:
                        view.changeBoatEnterType(changeBoat);
                        break;
                    case views.ChangeBoatEnterLength:
                        view.changeBoatEnterLength(changeBoat);
                        break;
                    case views.ChangeBoatSaved:
                        view.changeBoatSaved(changeBoat);
                        break;
                    case views.WrongInput:
                        view.viewWrongInput();
                        break;
                    default:
                        break;
                }
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


/*
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

                else if (changeBoatEnterType)
                {
                    currentView = views.ChangeBoatEnterType;
                    changeBoatEnterType = false;
                    changeBoatEnterLength = true;
                    changeBoat.UniqueId = userFeedback;
                    
                }

                else if (changeBoatEnterLength)
                {
                    changeBoat.Type = (Boat.type)Enum.Parse(typeof(Boat.type), userFeedback);
                    currentView = views.ChangeBoatEnterLength;

                    //Updating the registry text with the updated boat
                    string saveBoatText = saveChangedBoat(changeBoat);

                    //Saving the boat to the registry
                    writeToFile(saveBoatText);

                    changeBoatEnterLength = false;
                    changeBoatSaved = true;
                }

                else if (changeBoatSaved)
                {
                    currentView = views.ChangeBoatSaved;
                    changeBoatSaved = false;
                }
                */

//Show the regular menu options
/*
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
*/
