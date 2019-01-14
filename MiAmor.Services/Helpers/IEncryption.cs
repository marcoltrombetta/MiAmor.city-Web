﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Services
{
    public interface IEncryption
    {
         string Encrypt(string plainText);
         string Decrypt(string cipherText);
         string EncryptStrLink(string StrEnc);
         string DecryptStrLink(string StrDec);
    }
}
