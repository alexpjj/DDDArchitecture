using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Entities
{
    public sealed class Teacher : Base.Entity
    {
        public PersonalEmail PersonalEmail { get; }
        public ELearnEmail ProfessionalEmail { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Lastname { get; }       

        public Teacher(string name, string surname, string lastname, PersonalEmail personalEmail)
        {
            this.Name = name;
            this.Surname = surname;
            this.Lastname = lastname;
            this.PersonalEmail = personalEmail;
        }
    }
}
