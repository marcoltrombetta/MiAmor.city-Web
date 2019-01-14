using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class PriceListItem : Entity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        bool Active { get; set; }

        public int PriceListId { get; set; }
        public virtual PriceList PriceList { get; set; }
    }
}
