using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    class VendorMessage : Entity<int>
    {
        public int VendorId { get; set; }

        public int MessagetId { get; set; }

        public Vendor Vendor { get; set; }

        public Message Message{ get; set; }
    }
}
