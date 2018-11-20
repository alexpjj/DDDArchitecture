using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Application.IoC.Modules
{
    public class InfrastuctureRegistrationSettings : Autofac.Module
    {
        private readonly InfrastuctureSettings infrastuctureSettings;

        public InfrastuctureRegistrationSettings(InfrastuctureSettings infrastuctureSettings)
        {
            this.infrastuctureSettings = infrastuctureSettings;
        }
    }
}
