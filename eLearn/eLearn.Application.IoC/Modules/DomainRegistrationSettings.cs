using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Reflection;

namespace eLearn.Application.IoC.Modules
{
    public class DomainRegistrationSettings : Autofac.Module
    {
        private readonly DomainSettings domainSettings;

        public DomainRegistrationSettings(DomainSettings domainSettings)
        {
            this.domainSettings = domainSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            Assembly assembly = typeof(Domain.Contracts.Repositories.IStudentRepository).Assembly;
            
            base.Load(builder);
        }
    }
}
