using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Autofac;
using System.Data.Entity;
using MiAmor.Data;
using MiAmor.Core;

namespace MiAmor.Data
{
    public class MiAmorInitializer : CreateDatabaseIfNotExists<MiAmorContext>
    {
      
    }
}
