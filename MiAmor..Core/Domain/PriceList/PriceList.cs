using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class PriceList : Entity<int>
    {
        private List<PriceListItem> _PriceListItem;
        private List<PriceListVendorCategory> _PriceListVendorCategory;

       public string Name { get; set; }

       public bool Active { get; set; }

       public virtual List<PriceListItem> PriceListItem
       {
           get { return _PriceListItem ?? (_PriceListItem = new List<PriceListItem>()); }
           set { _PriceListItem = value; }
       }

       public virtual List<PriceListVendorCategory> PriceListVendorCategory
       {
           get { return _PriceListVendorCategory ?? (_PriceListVendorCategory = new List<PriceListVendorCategory>()); }
           set { _PriceListVendorCategory = value; }
       }
    }
}
