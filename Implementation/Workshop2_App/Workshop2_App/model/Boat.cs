using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop2_App.model
{
    public class Boat
    {
        public enum type
        {
            Sailboat,
            Motorsailer,
            Canoe,
            Other
        };

        private string length;

        public type Type
        {
            get;
            set;
        }

        public string Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }

    }
}
