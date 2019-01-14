using MiAmor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Models
{
    public class CustomerProfileCities
    {
        public CustomerProfile CustomerProfile { get; set; }
        public List<City> Cities { get; set; }
    }
}