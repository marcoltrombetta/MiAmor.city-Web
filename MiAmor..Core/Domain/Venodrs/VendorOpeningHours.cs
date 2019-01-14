using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiAmor.Core;

namespace MiAmor.Core
{
    public class VendorOpeningHours : Entity<int>
    {
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets the hours of the day
        /// </summary>
        public String Sunday { get; set; }

        /// <summary>
        /// Gets or sets the hours of the day
        /// </summary>
        public String Monday { get; set; }

        /// <summary>
        /// Gets or sets the hours of the day
        /// </summary>
        public String Tuesday { get; set; }

        /// <summary>
        /// Gets or sets the hours of the day
        /// </summary>
        public String Wednesday { get; set; }

        /// <summary>
        /// Gets or sets the hours of the day
        /// </summary>
        public String Thursday { get; set; }

        /// <summary>
        /// Gets or sets the hours of the day
        /// </summary>
        public String Friday { get; set; }

        /// <summary>
        /// Gets or sets the hours of the day
        /// </summary>
        public String Saturday { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
