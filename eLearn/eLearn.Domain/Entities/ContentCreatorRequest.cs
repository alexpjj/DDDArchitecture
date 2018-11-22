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
        public RequestStatus Status { get; private set; }
        public long ContentCreatorId { get; private set; }
        public string PetitionDescription { get; private set; }
        public long? ValidatorId { get; private set; }
        public string ValidationDescription { get; set; }


        protected ContentCreatorRequest(long contentCreatorId, string petitionDescription)
        {
            if (string.IsNullOrWhiteSpace(petitionDescription))
                throw new InvalidRequestCreationException();

            this.ContentCreatorId = contentCreatorId;
            this.PetitionDescription = petitionDescription;
            this.Status = RequestStatus.InProgress;
            //TODO add timespan last update date
        }
     
        protected void AcceptRequest(long validatorId, string validationDescription = "")
        {
            this.ValidatorId = validatorId;
            this.ValidationDescription = ValidationDescription;
        }

        protected void DeclinedRequest(long contentCreatorId, long validatorId, string validationDescription)
        {
            if (string.IsNullOrWhiteSpace(validationDescription))
                throw new InvalidDeclinedRequestException();

            this.Status = RequestStatus.Declined;
            this.ValidatorId = ValidatorId;
            this.ValidationDescription = validationDescription;
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
