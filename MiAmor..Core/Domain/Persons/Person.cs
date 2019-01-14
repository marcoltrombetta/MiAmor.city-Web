using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class Person :  Entity<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
       
        public bool? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string ImgPath { get; set; }

        public DateTime? MarriageDate { get; set; }

        public virtual PersonAddress PersonAddress { get; set; }
    }
}
