using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Autofac;
using MiAmor.Data;

namespace MiAmor.Builder
{
    

    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(MiAmorContext)).As(typeof(IContext)).InstancePerLifetimeScope();                 

        }

    }
}