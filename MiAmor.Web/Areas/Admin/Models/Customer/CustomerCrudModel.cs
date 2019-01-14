using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Areas.Admin.Models
{
    public class CustomerCrudModel
    {
        public int Id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public bool? gender { get; set; }

        public string phone { get; set; }

        public string mobile { get; set; }       

        public DateTime? dateOfBirth { get; set; }

        public string password { get; set; }

        public int cityId { get; set; }

        public string mainAddress { get; set; }

        public int customerTypeId { get; set; }

    }
}