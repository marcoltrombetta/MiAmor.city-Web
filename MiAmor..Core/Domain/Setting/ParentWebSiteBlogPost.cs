using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public partial class ParentWebSiteBlogPost : Entity<int>
    {
        public int ParentWebSiteSettingId { get; set; }

        public int BlogPostId { get; set; }

        public BlogPost BlogPost { get; set; }

        public ParentWebSiteSetting ParentWebSiteSetting { get; set; }
    }
}
