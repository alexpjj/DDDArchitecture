using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLearn.Domain.Entities;

namespace eLearn.Domain.Entities
{
    public sealed class User : Base.Entity
    {
        public string Name { get; }
        public string Surname { get; }
        public string Lastname { get; }
        public PersonalEmail PersonalEmail { get; }
        public ELearnEmail ELearnEmail { get; }

        public User(string name, string surname, string lastname, PersonalEmail personalEmail)
        {
            this.Name = name;
            this.Surname = surname;
            this.Lastname = lastname;
            this.PersonalEmail = personalEmail;
        }

        public User(string name, string surname, string lastname, string personalEmail)
        {
            this.Name = name;
            this.Surname = surname;
            this.Lastname = lastname;
            this.PersonalEmail = new PersonalEmail(personalEmail);
        }
    }
}
