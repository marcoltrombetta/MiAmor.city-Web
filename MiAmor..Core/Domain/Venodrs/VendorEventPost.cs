using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public partial class VendorEventPost : Entity<int>
    {
        public int VendorId { get; set; }

        public int EventPostId { get; set; }

        public Vendor Vendor { get; set; }

        public EventPost EventPost { get; set; }
    }
}
