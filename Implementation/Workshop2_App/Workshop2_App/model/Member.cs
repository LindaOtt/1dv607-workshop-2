using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop2_App.model
{
    class Member
    {
        private string name;
        private string personalNumber;
        private string uniqueId;
        private List<Boat> memberBoats;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string PersonalNumber
        {
            get
            {
                return personalNumber;
            }
            set
            {
                personalNumber = value;
            }
        }

        public string UniqueId
        {
            get
            {
                return uniqueId;
            }
            set
            {
                uniqueId = value;
            }
        }

        public List<Boat> MemberBoats
        {
            get
            {
                return memberBoats;
            }
            set
            {
                memberBoats = value;
            }
        }


        public List<Boat> getMemberBoats()
        {
            return memberBoats;
        }
    }
}
