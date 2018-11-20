using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Application.IoC.Modules
{
    public class ApplicationRegistrationSettings : Autofac.Module
    {
        private readonly ApplicationSettings applicationSettings;

        public ApplicationRegistrationSettings(ApplicationSettings applicationSettings)
        {
            this.applicationSettings = applicationSettings;
        }
    }
}
