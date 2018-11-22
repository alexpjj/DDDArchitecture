using AutoFixture.Kernel;
using eLearn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.TDD.Configuration.AutoFixture
{
    public class EmailAdressSpecimen : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var parameter = request as ParameterInfo;

            if (parameter == null)
                return new NoSpecimen();

            if (parameter.Member.DeclaringType == typeof(PersonalEmail))
                return new PersonalEmail("abc@gmail.com");

            if (parameter.Member.DeclaringType == typeof(ELearnEmail))
                return new ELearnEmail("abc@eLearn.com");
         
            return new NoSpecimen();            
        }        
    }
}
