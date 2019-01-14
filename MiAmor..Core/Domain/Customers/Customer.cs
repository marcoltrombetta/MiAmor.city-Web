using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class Customer : Person
    {
        public DateTime? RegistrationDate { get; set; }

        public DateTime? LastEnterDate { get; set; }
        
        public int? TokenId { get; set; }

        public string Password { get; set; }

        public string IpRegistration { get; set; }

        public string IpLastEnter { get; set; }

        public string AdminComment { get; set; }

        public bool? Active { get; set; }

        public bool? Deleted { get; set; }

        public string CustId { get; set; }

        public int? CustomerTypeId { get; set; }

        public string MainAddress { get; set; }

        public int? CityId { get; set; }

        public CustomerType CustomerType { get; set; }

        public City City { get; set; }
    }
}
