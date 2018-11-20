﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Application.IoC.Modules
{
    public class DomainRegistrationSettings : Autofac.Module
    {
        private readonly DomainSettings domainSettings;

        public DomainRegistrationSettings(DomainSettings domainSettings)
        {
            this.domainSettings = domainSettings;
        }
    }
}
