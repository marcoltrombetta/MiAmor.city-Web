using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class CustomerMessageTypes
    {
        public CustomerMessage CustomerMessage { get; set; }

        public List<CustomerMessageType> CustomerMessageType { get; set; }
    }
}