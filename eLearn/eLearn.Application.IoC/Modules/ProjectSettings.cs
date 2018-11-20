using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Application.IoC.Modules
{
    public class ProjectSettings
    {
        private readonly InfrastuctureSettings infrastuctureSettings;
        private readonly DomainSettings domainSettings;
        private readonly ApplicationSettings applicationSettings;

        public ProjectSettings(
            InfrastuctureSettings infrastuctureSettings, 
            DomainSettings domainSettings, 
            ApplicationSettings applicationSettings)
        {
            this.infrastuctureSettings = infrastuctureSettings;
            this.domainSettings = domainSettings;
            this.applicationSettings = applicationSettings;
        }
    }
}
