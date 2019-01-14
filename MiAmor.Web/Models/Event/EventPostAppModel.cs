using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class EventPostAppModel
    {

        public string Title { get; set; }
       

        public DateTime? StartDateUtc { get; set; }

        public ICollection<EventPostPicture> EventPostPictures { get; set; }

        public string Body { get; set; }

        public ICollection<EventComment> EventComments { get; set; }
    }
}