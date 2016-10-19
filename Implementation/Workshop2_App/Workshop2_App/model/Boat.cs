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
        private string uniqueId;
        private int orderNumber;

        public type Type
        {
            get;
            set;
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

        public int OrderNumber
        {
            get
            {
                return orderNumber;
            }
            set
            {
                orderNumber = value;
            }
        }

    }
}
