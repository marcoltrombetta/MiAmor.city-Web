using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Models
{
    public class CustomerProfile
    {
        public int Id { set; get; }
        public string CustId { set; get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool? Gender { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string ImgPath { get; set; }

        public DateTime? MarriageDate { get; set; }
        public string Password { get; set; }

        public string MainAddress { get; set; }

        public int? CityId { get; set; }

    }
}