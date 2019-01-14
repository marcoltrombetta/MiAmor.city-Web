using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public partial class Message : Entity<int>
    {
        public int CustomerId { get; set; }

        public int ParentIssueId { get; set; }

        public bool IsOwner { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdateOnUtc { get; set; }

        public int SubjectId { get; set; }

        public string Body { get; set; }

        public int StatusId { get; set; }

        public int OpenEmployee { get; set; }

        public int CurrEmployeeAttend { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string SiteUrl{ get; set; }

    }
}
