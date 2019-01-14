using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class PriceListVendorCategory : Entity<int>
    {
        public int PriceListId { get; set; }

        public int VendorCategoryId { get; set; }

        public virtual PriceList PriceList { get; set; }

        public virtual VendorCategory VendorCategory { get; set; }

    }
}
