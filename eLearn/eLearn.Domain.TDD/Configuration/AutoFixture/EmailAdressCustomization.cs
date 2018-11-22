using AutoFixture;
using eLearn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.TDD.Configuration.AutoFixture
{
    public class EmailAdressCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            /*fixture.Customize<Email>(custom => custom.With(x => x.ElectronicAdress, "abc@gmail.com"));
            fixture.Customize<PersonalEmail>(custom => custom.With(x => x.ElectronicAdress, "abc@gmail.com"));
            fixture.Customize<ELearnEmail>(custom => custom.With(x => x.ElectronicAdress, "abc@eLearn.com"));*/
        }
    }
}
