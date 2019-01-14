using MiAmor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MiAmor.Web.Models
{
    public class SearchModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<string> SubTitle { get; set; }

        public string ShortDescription{ get; set; }

        public string Description { get; set; }

        public string LinkToPage{ get; set; }

        public string ExtraText { get; set; }
        public int SearchType { get; set; }

        public List<Picture> Pictures { get; set; }

    }
}