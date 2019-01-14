using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class ContactUsModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string SiteUrl { get; set; }

    }
}