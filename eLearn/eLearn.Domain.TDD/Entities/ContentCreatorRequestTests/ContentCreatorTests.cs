using eLearn.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using eLearn.Domain.Enums;
using eLearn.Domain.Exceptions;

namespace eLearn.Domain.TDD.Entities.ContentCreatorRequestTests
{
    [TestFixture]
    public class ContentCreatorTests
    {
        [Test]
        public void ShouldCreateAnAcceptedContentCreatorRequest()
        {
            ContentCreatorRequest acceptedRequest;

            acceptedRequest = ContentCreatorRequest.AcceptedRequest(1, 1, "He is a Microsoft MVP, so why not");

            acceptedRequest.Status.Should().Be(RequestStatus.Accepted);
        }

        [Test]
        public void ShouldCreateADeclinedContentCreatorRequest()
        {
            ContentCreatorRequest declinedRequest;

            declinedRequest = ContentCreatorRequest.DeclinedRequest(1, 1, "Not valid");

            declinedRequest.Status.Should().Be(RequestStatus.Declined);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("      ")]
        [TestCase((string)null)]
        [Test]
        public void ShouldNotCreateAnEmptyDescriptionWhenDeclined(string description)
        {
            Action declinedRequest = () => ContentCreatorRequest.DeclinedRequest(1, 1, description);

            declinedRequest.Should().Throw<InvalidDeclinedRequestException>();
        }

    }
}

