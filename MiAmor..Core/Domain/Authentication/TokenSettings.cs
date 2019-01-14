using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiAmor;

namespace MiAmor.Core
{
    public class TokenSettings : Entity<int>
    {
        public string passPhrase { get; set; }

        public string saltValue { get; set; }

        public string hashAlgorithm { get; set; }

        public string initVector { get; set; }

        public int passwordIterations { get; set; }

        public int keySize { get; set; }

        public int ExpirationDaysShort { get; set; }

        public int ExpirationDaysLong { get; set; }

        public int ExpirationDaysNull { get; set; }
    }
}
