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
        private int personalNumber;
        private string uniqueId;

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

        public int PersonalNumber
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
    }
}
