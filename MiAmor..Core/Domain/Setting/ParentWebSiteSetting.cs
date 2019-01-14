
using MiAmor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public partial class ParentWebSiteSetting: Entity<int>
    {
        private ICollection<ParentWebSiteBlogPost> _ParentWebSiteBlogPosts;

        public string Name { get; set; }


        public string AboutUsShort { get; set; }

        public string AboutUsFull { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }


        public virtual ICollection<ParentWebSiteBlogPost> ParentWebSiteBlogPosts
        {
            get { return _ParentWebSiteBlogPosts ?? (_ParentWebSiteBlogPosts = new List<ParentWebSiteBlogPost>()); }
            protected set { _ParentWebSiteBlogPosts = value; }
        }

    }
}
