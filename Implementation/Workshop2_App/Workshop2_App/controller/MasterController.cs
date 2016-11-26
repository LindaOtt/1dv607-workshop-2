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
        private DataController dataController = new DataController();

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
                            string uniqueId = memberController.generateId();
                            memberController.setMemberFromInput(input, uniqueId, changeMember);

                            enterPNumberControl = false;

                            //Adding the member to the database
                            string newText = dataController.changeMemberRegistry(changeMember, "add");
                            
                            //Adding the new text to the registry
                            dataController.writeToFile(newText);

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
                            string writeLine = dataController.changeMemberRegistry(changeMember, "change");

                            dataController.writeToFile(writeLine);
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
                            
                            string newText = dataController.addBoatToMemberInRegistry(changeMember, changeBoat);

                            dataController.writeToFile(newText);

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
                                    
                                    string newBoatsText = dataController.changeBoatRegistry(changeBoat, "delete");

                                    dataController.writeToFile(newBoatsText);
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

                            string saveBoatText = dataController.changeBoatRegistry(changeBoat, "change");

                            //Saving the boat to the registry
                            dataController.writeToFile(saveBoatText);

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

        
    }
}