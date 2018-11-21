using eLearn.Domain.Enums;
using eLearn.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Entities
{
    public class ContentCreatorRequest : Base.ValueObject<ContentCreatorRequest>
    {
        public RequestStatus Status { get; }
        public long ContentCreatorId { get; }
        public string PetitionDescription { get; }
        public long? ValidatorId { get; }
        public string ValidationDescription { get; }

        public ContentCreatorRequest(long contentCreatorId, string petitionDescription)
        {
            if (string.IsNullOrWhiteSpace(petitionDescription))
                throw new InvalidRequestCreationException();

            this.ContentCreatorId = contentCreatorId;
            this.PetitionDescription = petitionDescription;
            this.Status = RequestStatus.InProgress;
            //TODO add timespan last update date
        }

        private ContentCreatorRequest(RequestStatus status, long contentCreatorId, long? validatorId, string validationDescription)
        {
            this.Status = status;
            this.ContentCreatorId = contentCreatorId;
            this.ValidatorId = validatorId;
            this.ValidationDescription = validationDescription;
        }

        protected static ContentCreatorRequest AcceptedRequest(long contentCreatorId, long validatorId, string validationDescription = "")
        {
            return new ContentCreatorRequest(RequestStatus.Accepted, contentCreatorId, validatorId, validationDescription);
        }

        protected static ContentCreatorRequest DeclinedRequest(long contentCreatorId, long validatorId, string validationDescription)
        {
            if (string.IsNullOrWhiteSpace(validationDescription))
                throw new InvalidDeclinedRequestException();

            return new ContentCreatorRequest(RequestStatus.Declined, contentCreatorId, validatorId, validationDescription);
        }
        

        protected override bool EqualsCore(ContentCreatorRequest other)
        {
            return this.ContentCreatorId == other.ContentCreatorId && this.ValidatorId == other.ValidatorId;
        }

        protected override int GetHashCodeCore()
        {
            var validator = this.ValidatorId.HasValue ? this.ValidatorId.ToString() : string.Empty;
            return (this.ContentCreatorId.ToString() + validator).GetHashCode();
        }
    }
}
