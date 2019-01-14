using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;

using MiAmor.Services;
using MiAmor.Data;
using MiAmor.Core;
using MiAmor.Web.Models;
using MiAmor.Web.Filters;
using System.ComponentModel;


namespace MiAmor.Web.Helpers
{
    public class Authentication
    {
        
        public Authentication()
        {
        
        }

        public int IsTokenAuthenticated(string StrTokenToValidate)
        {
            MiAmorContext db = new MiAmorContext();
            string TmpStrTokenToValidate=StrTokenToValidate.Replace("Bearer ","");
            //TmpStrTokenToValidate = TmpStrTokenToValidate.Substring(2, TmpStrTokenToValidate.Length - 4);
            Token TryToken = (from t in db.Token
                              where t.EncryptedToken == TmpStrTokenToValidate &&
                              t.ExpirationDate > DateTime.Now
                              select t).FirstOrDefault();
            if (TryToken != null)
                return TryToken.Id;
            else
                return 0;   
        }
    }
}