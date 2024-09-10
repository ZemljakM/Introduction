using Autofac;
using Introduction.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Repository
{
    public class RepositoryDIModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClubRepository>().As<IClubRepository>().InstancePerDependency();
            builder.RegisterType<ClubPresidentRepository>().As<IClubPresidentRepository>().InstancePerDependency();
        }
    }
}
