using eLearn.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Entities
{
    public class ElearnValidator : Base.Entity
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Lastname { get; private set; }

        public virtual List<ContentCreatorRequest> Requests { get; private set; }

        public ElearnValidator(string name, string surname, string lastname)
        {
            this.Name = name;
            this.Surname = surname;
            this.Lastname = lastname;
            this.Requests = new List<ContentCreatorRequest>();
        }

        public void GrabRequest(ContentCreatorRequest request)
        {
            request.GrabRequest(this.Id);

            this.Requests.Add(request);
        }

        public void ValidateRequest(long contentCreatorId, string description)
        {
            var request = this.FindRequest(contentCreatorId);

            if (request.Status != Enums.RequestStatus.InProgress)
                throw new RequestValidationException();

            request.AcceptRequest(this.Id, description);
        }

        public void DeclineRequest(long contentCreatorId, string validationDescription)
        {
            var request = this.FindRequest(contentCreatorId);

            request.DeclineRequest(this.Id, validationDescription);
        }

        private ContentCreatorRequest FindRequest(long contentCreatorId)
        {
            var request = this.Requests.Find(x => x.ContentCreatorId == contentCreatorId);

            if (request == null)
                throw new EntityNotFoundException();

            return request;
        }
    }
}
