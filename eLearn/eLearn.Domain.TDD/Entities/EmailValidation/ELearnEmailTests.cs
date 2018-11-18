using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using eLearn.Domain.Entities;

namespace eLearn.Domain.TDD.Entities.EmailValidation
{
    [TestFixture]
    public class ELearnEmailTests
    {
        [TestCase("bbb@eLearn.com")]
        [TestCase("BBB@elearn.com")]       
        public void ShoudlBeValid(string email)
        {
            ELearnEmail eLearnEmail;

            eLearnEmail = new ELearnEmail(email);

            ELearnEmail.IsValid(email).Should().Be(true);
            eLearnEmail.ElectronicAdress.Should().Be(email);
        }
        
        [TestCase("a@elearn1.com")]
        [TestCase("b@elearn.es")]
        public void ShoudlNotBeValid(string email)
        {
            Action action = () => new ELearnEmail(email);

            ELearnEmail.IsValid(email).Should().Be(false);
            action.Should().Throw<Exceptions.ELearnEmailValidationException>(email);
        }
    }
}
