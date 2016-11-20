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
        private MemberList memberList = new MemberList();

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
                    changeMember.Name = userFeedback;
                    break;
                case "memberEnterPNumber":
                
                    changeMember.PersonalNumber = userFeedback;
                    break;
                case "memberCreateSave":
                    changeMember.UniqueId = userFeedback;
                    break;
                case "memberChangeSetId":
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        if (number > 0)
                        {
                            Debug.Write("The user entered the number ");
                            Debug.Write(userFeedback);
                            Debug.WriteLine(" ");
                            string uniqueId = this.memberList.getUniqueId(Int32.Parse(userFeedback));
                            changeMember.UniqueId = uniqueId;
                            //changeMember.UniqueId = "000";
                        }
                    }
                    break;
                case "memberChangeName":
                    changeMember.Name = userFeedback;
                    break;
                case "memberChangePNumber":
                    changeMember.PersonalNumber = userFeedback;
                    break;
                case "memberLookAtPick":
                    if (Int32.TryParse(userFeedback, out number))
                    {
                        if (number > 0)
                        {
                            
                            changeMember.UniqueId = this.memberList.getUniqueId(Int32.Parse(userFeedback));
                        }
                    }
                    break;
                default:
                    Debug.WriteLine("inputFromUser: default");
                    break;
            }

            member = changeMember;
        }

    }
}
