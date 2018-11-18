using eLearn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eLearn.Domain.Entities
{
    public class Email : Base.ValueObject<Email>
    {
        public string ElectronicAdress { get; protected set; }


        public Email(string electronicAdress)
        {
            this.ElectronicAdress = Validate(electronicAdress)
                ? electronicAdress : throw new Exceptions.PersonalEmailValidationException();
        }

        protected static bool Validate(string electronicAdress)
        {
            return Regex.IsMatch(electronicAdress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
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
