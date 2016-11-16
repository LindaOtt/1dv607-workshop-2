using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2_App.view;
using Workshop2_App.model;
using System.Diagnostics;

namespace Workshop2_App.controller
{
    class MemberController
    {
        private string inputFromUser;
        private string userFeedback;
        int number;
        private Member changeMember;
        private MemberList memberList;

        public Member getMember(string input, string userFeedback)
        {
            inputFromUser = input;
            return changeMember;
        }

        public MemberController(Member member) {
            changeMember = member;

            switch (inputFromUser) {
                case "memberEntername":
                    Console.WriteLine("Inside memberEnterName");
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        if (number > 0)
                        {
                            //currentView = views.ChangeMemberEnterName;
                            changeMember.UniqueId = memberList.getUniqueId(Int32.Parse(userFeedback));
                        }
                        else
                        {
                            //currentView = views.showFirstView;
                        }
                    }
                    break;
                case "memberEnterPNumber":
                    break;
                case "memberChangeName":
                    break;
                case "memberChangePNumber":
                    break;
                case "memberSave":
                    break;
                case "memberLookAtPick":
                    break;
                default:
                    Debug.WriteLine("Default inputFromUser");
                    break;
            } 
        }
    }
}
