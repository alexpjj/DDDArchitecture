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
using eLearn.Application.IoC.Interceptors;

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
            Assembly providersAssembly = typeof(DTO.CreateUserDto).Assembly;

            builder.RegisterAssemblyTypes(providersAssembly)
               .Where(x => typeof(FluentValidation.IValidator).IsAssignableFrom(x))
               .AsImplementedInterfaces()
               .Keyed<FluentValidation.IValidator>(x => x.BaseType.GenericTypeArguments[0])
               .SingleInstance();

            builder.RegisterAssemblyTypes(providersAssembly)
               .Where(x => typeof(Providers.UserService).IsAssignableFrom(x))
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(Interceptors.Logger), typeof(Validation))
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
            builder.RegisterType<Interceptors.Validation>();

            base.Load(builder);
        }
    }
}
