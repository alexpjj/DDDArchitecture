using eLearn.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eLearn.Domain.Entities
{
    public sealed class ELearnEmail : Email
    {
        public ELearnEmail(string electronicAdress) : base(electronicAdress)
        {
            ElectronicAdress = IsValid(electronicAdress)
                ? electronicAdress : throw new Exceptions.ELearnEmailValidationException();
        }

        public static bool IsValid(string electronicAdress)
        {
            return Email.Validate(electronicAdress) && electronicAdress.Substring(electronicAdress.IndexOf("@")).ToLower() == "@eLearn.com".ToLower();
        }

        protected override int GetHashCodeCore()
        {
            return this.ElectronicAdress.GetHashCode();
        }

        protected override bool EqualsCore(Email other)
        {
            return this.ElectronicAdress == other.ElectronicAdress;
        }
    }
}
