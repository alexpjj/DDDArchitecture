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
        public long ValidatorId { get; }
        public string ValidationDescription { get; }

        private ContentCreatorRequest(RequestStatus status, int contentCreatorId, int validatorId, string validationDescription)
        {
            this.Status = status;
            this.ContentCreatorId = contentCreatorId;
            this.ValidatorId = validatorId;
            this.ValidationDescription = validationDescription;
        }

        public static ContentCreatorRequest AcceptedRequest(int contentCreatorId, int validatorId, string validationDescription = "")
        {
            return new ContentCreatorRequest(RequestStatus.Accepted, contentCreatorId, validatorId, validationDescription);
        }

        public static ContentCreatorRequest DeclinedRequest(int contentCreatorId, int validatorId, string validationDescription)
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
            return (this.ContentCreatorId.ToString() + this.ValidatorId.ToString()).GetHashCode();
        }
    }
}
