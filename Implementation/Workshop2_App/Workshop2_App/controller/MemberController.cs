using System;
using System.IO;
using Workshop2_App.model;
using System.Diagnostics;

namespace Workshop2_App.controller
{
    class MemberController
    {
        private int number;
        private Member member;
        private MemberList memberList = new MemberList();

        public Member getMember()
        {
            return member;
        }

        public void setMemberFromInput(string input, string userFeedback, Member changeMember)
        {

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
                            string uniqueId = this.memberList.getUniqueId(Int32.Parse(userFeedback));
                            changeMember.UniqueId = uniqueId;
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
                    
                    break;
            }

            member = changeMember;
        }

        public string generateId()
        {
            long currentTime = DateTime.Now.Ticks;
            string id = currentTime.ToString();
            return id;
        }

    }
}
