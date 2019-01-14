using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public partial class PictureSizeType : Entity<int>
    {
        public string Name { get; set; }      

        public bool? IsDeleted { get; set; }

        public bool? IsActive { get; set; }


    }
}
