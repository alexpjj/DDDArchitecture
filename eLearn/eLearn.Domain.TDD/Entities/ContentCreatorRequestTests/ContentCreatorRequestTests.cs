using eLearn.Domain.Entities;
using eLearn.Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.TDD.Entities.ContentCreatorRequestTests
{
    [TestFixture]
    public class ContentCreatorRequestTests : TestBase
    {
        [Test]
        public void NewRequestShouldBeOnHold()
        {
            long contentCreatorId = 1;
            string petitionDescription = "Moneeeeeeeeey";
            var request = new ContentCreatorRequest(contentCreatorId, petitionDescription);

            request.Status.Should().Be(Enums.RequestStatus.OnHold);
            request.ContentCreatorId.Should().Be(contentCreatorId);
            request.PetitionDescription.Should().Be(petitionDescription);
        }

        [Test]
        public void NewRequestShouldHaveDescription()
        {
            Action request = () => new ContentCreatorRequest(1, string.Empty);

            request.Should().Throw<InvalidRequestCreationException>();
        }

        [Test]
        public void ShouldGrabRequest()
        {
            var request = this.Create<ContentCreatorRequest>();
            long validatorId = 1;

            request.GrabRequest(validatorId);

            request.Status.Should().Be(Enums.RequestStatus.InProgress);
            request.ValidatorId.Should().Be(validatorId);
        }

        [Test]
        public void GrabbedRequestShouldBeOnHold()
        {
            var request = this.Create<ContentCreatorRequest>();
            request.AcceptRequest(1, "asd");

            Action grabbedRequest = () => request.GrabRequest(1);

            grabbedRequest.Should().Throw<RequestAssignmentException>();
        }

        [Test]
        public void SholdAcceptRequest()
        {
            var request = this.Create<ContentCreatorRequest>();
            long validatorId = 1;
            string validationDescription = "asd";

            request.AcceptRequest(validatorId, validationDescription);

            request.Status.Should().Be(Enums.RequestStatus.Accepted);
            request.ValidatorId.Should().Be(validatorId);
            request.ValidationDescription.Should().Be(validationDescription);
        }

        [Test]
        public void ShouldDeclineRequest()
        {
            var request = this.Create<ContentCreatorRequest>();
            long validatorId = 1;
            var validationDescription = "asd";

            request.DeclineRequest(validatorId, validationDescription);

            request.Status.Should().Be(Enums.RequestStatus.Declined);
            request.ValidatorId.Should().Be(validatorId);
            request.ValidationDescription.Should().Be(validationDescription);
        }

        [Test]
        public void DeclinedRequestShouldHaveAValidationDescription()
        {
            var request = this.Create<ContentCreatorRequest>();

            Action declinedRequest = () => request.DeclineRequest(1, string.Empty);

            declinedRequest.Should().Throw<InvalidDeclinedRequestException>();
        }
    }
}
