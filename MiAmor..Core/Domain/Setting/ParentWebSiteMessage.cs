using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    class ParentWebSiteMessage : Entity<int>
    {
        public int ParentWebSiteSettingId { get; set; }

        public int MessageId { get; set; }

        public Message Message { get; set; }

        public ParentWebSiteSetting ParentWebSiteSetting { get; set; }
    }
}
