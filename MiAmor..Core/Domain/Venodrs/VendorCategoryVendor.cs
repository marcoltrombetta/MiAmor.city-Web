using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public partial class VendorCategoryVendor : Entity<int>
    {
        public int VendorId { get; set; }

        public int VendorCategoryId { get; set; }

        public Vendor Vendor { get; set; }

        public VendorCategory VendorCategory { get; set; }

    }
}
