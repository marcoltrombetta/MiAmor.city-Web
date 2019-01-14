﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class CustomerMessageType : Entity<int>
    {
        public string Name { get; set; }

        public bool? Active { get; set; }

        public bool? Deleted { get; set; }

    }
}
