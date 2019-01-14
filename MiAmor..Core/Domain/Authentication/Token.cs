using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiAmor;

namespace MiAmor.Core
{
    public class Token : Entity<int>
    {
        public string EncryptedToken { get; set; }

        public int? TokenType { get; set; }
        
        public DateTime ExpirationDate { get; set; }

        public DateTime DateCreated  { get; set; }
        
    }
}
