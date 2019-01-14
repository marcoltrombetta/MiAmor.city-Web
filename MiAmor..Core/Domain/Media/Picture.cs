using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public partial class Picture : Entity<int> 
    {      
        public string BaseUrl { get; set; }

        public int? PictureSizeTypeId { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsActive { get; set; }
       
    }
}
