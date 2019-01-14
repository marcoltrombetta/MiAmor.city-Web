using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class EventPostTagCrudModel
    {
        public int Id { get; set; }

        public string name { get; set; }

        public int eventPostCount { get; set; }
    }
}