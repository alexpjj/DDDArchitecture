using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eLearn.Domain.Entities
{
    public sealed class PersonalEmail : Email
    {
        public PersonalEmail(string electronicAdress) : base(electronicAdress)
        {
            this.ElectronicAdress = Validate(electronicAdress)
                ? electronicAdress : throw new Exceptions.PersonalEmailValidationException();
        }
        
        public static bool IsValid(string electronicAdress)
        {
            return Email.Validate(electronicAdress);
        }

        protected override bool EqualsCore(Email other)
        {
            return this.ElectronicAdress == other.ElectronicAdress;   
        }

        protected override int GetHashCodeCore()
        {
            return this.ElectronicAdress.GetHashCode();         
        }
    }
}
