using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Models.Event
{
    public class EventMessageModel
    {
        public int EventPostId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string CommentText { get; set; }
    }
}