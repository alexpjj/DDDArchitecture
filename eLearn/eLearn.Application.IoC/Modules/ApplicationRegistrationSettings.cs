using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using System.Reflection;
using eLearn.Application.IoC;
using log4net;

namespace eLearn.Application.IoC.Modules
{
    public class ApplicationRegistrationSettings : Autofac.Module
    {
        private readonly ApplicationSettings applicationSettings;

        public ApplicationRegistrationSettings(ApplicationSettings applicationSettings)
        {
            this.applicationSettings = applicationSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            Assembly providersAssembly = typeof(DTO.CreateStudentDto).Assembly;

            builder.RegisterAssemblyTypes(providersAssembly)
               .Where(x => typeof(Providers.StudentService).IsAssignableFrom(x))
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(Interceptors.Logger))
               .OnPreparing(x =>
               {
                   x.Parameters = x.Parameters.Union(new[]
                   {
                        new TypedParameter(typeof (ILog), LogManager.GetLogger(x.Component.Activator.LimitType.Name))
                   });
               })
               .AsImplementedInterfaces();

            builder.Register(x => LogManager.GetLogger("Main"))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<Interceptors.Logger>();

            base.Load(builder);
        }
    }
}
