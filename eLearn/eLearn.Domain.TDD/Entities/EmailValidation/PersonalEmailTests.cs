using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using eLearn.Domain.Entities;
using FluentAssertions;
using eLearn.Domain;

namespace eLearn.Domain.TDD.Entities
{
    [TestFixture]
    public class PersonalEmailTests
    {
        [TestCase("aaa@gmail.com")]
        [TestCase("AAA@gmail.com")]
        [TestCase("aaa@fastmail.com")]
        public void ShoudlBeValid(string email)
        {
            PersonalEmail personalEmail;

            personalEmail = new PersonalEmail(email);
            
            PersonalEmail.IsValid(email).Should().Be(true);
            personalEmail.ElectronicAdress.Should().Be(email);
        }

        [TestCase("aaa@gmail..com")]
        [TestCase("AAA@.com")]
        [TestCase("aaafastmail.com")]
        [TestCase("")]
        public void ShoudlNotBeValid(string email)
        {
            Action action = () => new PersonalEmail(email);

            PersonalEmail.IsValid(email).Should().Be(false);
            action.Should().Throw<Exceptions.PersonalEmailValidationException>(email);
        }
    }
}
