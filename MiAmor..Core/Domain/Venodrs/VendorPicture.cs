using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class VendorPicture : Entity<int>
    {
        public int VendorId { get; set; }

        public int PictureId { get; set; }

        public int? DisplayOrder { get; set; }

        /// <summary>
        /// Gets the picture
        /// </summary>
        public virtual Picture Picture { get; set; }

        public virtual Vendor Vendor { get; set; }
      

    }
}
