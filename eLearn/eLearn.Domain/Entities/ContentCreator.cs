using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Entities
{
    public class ContentCreator : Base.Entity
    {
        public PersonalEmail PersonalEmail { get; private set; }
        public ELearnEmail ProfessionalEmail { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Lastname { get; private set; }

        public virtual ContentCreatorRequest Request { get; private set; }

        public virtual IEnumerable<Course> Courses { get; set; }      

        public ContentCreator(string name, string surname, string lastname, string personalEmail)
        {
            this.Name = name;
            this.Surname = surname;
            this.Lastname = lastname;
            this.PersonalEmail = new PersonalEmail(personalEmail);
            this.Courses = new List<Course>();
        }

        public void CreateRequest(string petitionDescription)
        {
            if (this.HasRequest())
                throw new InvalidOperationException("User already has a request in process");

            this.Request = new ContentCreatorRequest(this.Id, petitionDescription);
        }

        public bool HasRequest()
        {
            return this.Request != null;
        }
    }
}
