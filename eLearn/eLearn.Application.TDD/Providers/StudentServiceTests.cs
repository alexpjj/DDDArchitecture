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
    public class StudentServiceTests : ServiceTestBase<StudentService>
    {
        [Test]
        public void ShouldCreateStudent()
        {
            var student = this.Create<DTO.CreateStudentDto>(x => x.Email = "a@gmail.com");
            this.Mock<IStudentRepository>().Setup(x => x.Create(It.IsAny<Student>()));
           
            var result = this.service.Create(student);
            Task.WaitAll(result);

            this.VerifyOnce<IStudentRepository>(x => x.Create(It.IsAny<Student>()));
            result.IsCompleted.Should().Be(true);            
        }
    }
}
