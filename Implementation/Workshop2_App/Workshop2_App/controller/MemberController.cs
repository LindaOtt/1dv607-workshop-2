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
        private Member member;
        private MemberList memberList;

        public Member getMember()
        {
            return member;
        }

        public void setMemberFromInput(string input, string userFeedback, Member changeMember)
        {
            Debug.Write("SetMemberFromInput input: ");
            Debug.Write(input);
            Debug.WriteLine(" ");

            switch (input)
            {
                case "memberEnterName":
                    Debug.WriteLine("input: memberEnterName");
                    Debug.Write("userFeedback Name: ");
                    Debug.Write(userFeedback);
                    changeMember.Name = userFeedback;
                    break;
                case "memberEnterPNumber":
                    Debug.WriteLine("inputFromUser: memberEnterPNumber");
                    changeMember.PersonalNumber = userFeedback;
                    break;
                case "memberCreateSave":
                    Debug.WriteLine("inputFromUser: memberCreateSave");
                    changeMember.UniqueId = userFeedback;
                    break;
                case "memberChangeName":
                    break;
                case "memberChangePNumber":
                    break;
                case "memberLookAtPick":
                    break;
                default:
                    Debug.WriteLine("inputFromUser: default");
                    break;
            }

            member = changeMember;
        }

    }
}
