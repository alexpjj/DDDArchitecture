using eLearn.Application.Providers;
using eLearn.Domain.Contracts.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLearn.Domain.Entities;
using eLearn.Application;
using FluentAssertions;
using System.Threading;
using eLearn.Domain.Core;

namespace eLearn.Application.TDD.Providers
{
    [TestFixture]
    public class StudentServiceTests : ServiceTestBase<UserService>
    {
        [Test]
        public void ShouldCreateUser()
        {
            var user = this.Create<DTO.CreateUserDto>(x => x.Email = "a@gmail.com");
            this.Mock<IUserRepository>().Setup(x => x.Create(It.IsAny<User>()));
           
            var result = this.service.Create(user);
            Task.WaitAll(result);

            this.VerifyOnce<IUserRepository>(x => x.Create(It.IsAny<User>()));
            result.IsCompleted.Should().Be(true);            
        }
    }
}
