using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class CustomerBookmark : Entity<int>
    {
        public int CustomerId { get; set; }
        
        public int VendorId { get; set; }

        public Vendor Vendor { get; set; }
    }
}
