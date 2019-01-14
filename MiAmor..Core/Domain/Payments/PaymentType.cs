using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiAmor.Core;


namespace MiAmor.Core
{
    public class PaymentType : Entity<int>
    {
        public string Name{ get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the row was Deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the row is Active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

 
    }
}
