using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using eLearn.Application.IoC.Serializer;
using System.Reflection;
using Castle.DynamicProxy;

namespace eLearn.Application.IoC.Interceptors
{
    public class Logger : IInterceptor
    {
        private readonly ILog log;
        private readonly ISerializer serializer;

        public Logger(ILog log, ISerializer serializer)
        {
            this.log = log;
            this.serializer = serializer;
        }

        public void Intercept(IInvocation invocation)
        {
            this.log.InfoFormat("--- {0} start --- {1}",
                invocation.Method.Name,
                this.GetParameterString(invocation.GetConcreteMethod(), invocation.Arguments));
            
            try
            {
                invocation.Proceed();

                if (invocation.Method.ReturnType == null)
                {
                    this.log.Info("Return");
                }
                else
                {
                    this.log.InfoFormat("Return: {0}", this.serializer.Serialize(invocation.ReturnValue));
                }
            }
            catch (Exception ex)
            {
                this.log.Error(string.Format("Error in {0}", nameof(invocation.Method.Name)), ex);
                throw;
            }
        }

        private object GetParameterString(MethodInfo methodInfo, object[] arguments)
        {
            var parameters = methodInfo.GetParameters();

            if (parameters.Any())
            {
                var stringBuilder = new StringBuilder();

                stringBuilder.Append("Input parameters: ");
                for (int i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var argument = arguments[i];

                    stringBuilder.AppendFormat("{0}={1},", parameter.Name, this.serializer.Serialize(argument));
                }

                return stringBuilder.ToString();
            }

            return string.Empty;
        }
    }
}
