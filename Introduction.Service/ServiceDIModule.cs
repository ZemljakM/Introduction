using Autofac;
using Introduction.Repository.Common;
using Introduction.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class ServiceDIModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClubService>().As<IClubService>().InstancePerDependency();
            builder.RegisterType<ClubPresidentService>().As<IClubPresidentService>().InstancePerDependency();
        }
    }
}
