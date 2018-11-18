using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eLearn.Domain.Entities
{
    public sealed class PersonalEmail : Base.ValueObject<PersonalEmail>
    {
        public string ElectronicAdress { get; }


        public PersonalEmail(string electronicAdress)
        {
            this.ElectronicAdress = Regex.IsMatch(electronicAdress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase)
                ? electronicAdress : throw new Exceptions.PersonalEmailException();
        }

        protected override bool EqualsCore(PersonalEmail other)
        {
            return this.ElectronicAdress == other.ElectronicAdress;   
        }

        protected override int GetHashCodeCore()
        {
            return this.ElectronicAdress.GetHashCode();         
        }
    }
}
