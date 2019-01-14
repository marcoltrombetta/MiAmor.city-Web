using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiAmor.Core;


namespace MiAmor.Core
{
    public class VendorPaymentType : Entity<int>
    {
        public int VendorId { get; set; }

        public int PaymentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        public virtual PaymentType PaymentType { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
