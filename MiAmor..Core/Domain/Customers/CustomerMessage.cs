using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class CustomerMessage : Entity<int>
    {
        public int CustomerId { get; set; }

        public int VendorId { get; set; }

        public int CustomerMessageTypeId { get; set; }

        public string Title { get; set; }

        public string MsgTxt { get; set; }

        public bool WasOpened { set; get; }

        public bool Deleted { set; get; }

        public DateTime DateSend { get; set; }

        public DateTime DateOpened { get; set; }
        public CustomerMessageType CustomerMessageType { get; set; }

        public Vendor Vendor { get; set; }
    }
}
