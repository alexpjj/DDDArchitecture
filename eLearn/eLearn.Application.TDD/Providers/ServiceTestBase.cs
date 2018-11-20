using Autofac.Extras.Moq;
using AutoFixture;
using eLearn.Domain.Contracts.Repositories;
using eLearn.Domain.Core;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Application.TDD.Providers
{
    public class ServiceTestBase<T> where T : class
    {
        protected T service;

        protected Autofac.Extras.Moq.AutoMock moqer;

        protected Fixture fixture;

        [SetUp]
        public void Init()
        {
            this.moqer = AutoMock.GetLoose();
            this.fixture = new Fixture();

            this.Mock<Domain.Core.IUnitOfWork>().Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);
            this.Mock<IStudentRepository>().Setup(x => x.IUnitOfWork).Returns(this.Mock<IUnitOfWork>().Object);

            this.Configure();

            this.service = this.CreateService();
        }

        protected virtual T CreateService()
        {
            return this.moqer.Create<T>();
        }

        protected virtual void Configure()
        {
        }

        protected Moq.Mock<TDependency> Mock<TDependency>() where TDependency : class
        {
            return this.moqer.Mock<TDependency>();
        }

        protected TClass Create<TClass>()
        {
            return this.fixture.Create<TClass>();
        }

        protected TClass Create<TClass>(Action<TClass> fill)
        {
            var result = this.fixture.Create<TClass>();

            fill(result);

            return result;
        }

        protected IEnumerable<TClass> CreateMany<TClass>(int count = 2)
        {
            return this.fixture.CreateMany<TClass>(count);
        }

        protected void Verify<TDependency>(Expression<Action<TDependency>> expression, Times times) where TDependency : class
        {
            this.Mock<TDependency>().Verify(expression, times);
        }

        protected void Verify<TDependency>(Expression<Action<TDependency>> expression) where TDependency : class
        {
            this.Mock<TDependency>().Verify(expression);
        }

        protected void VerifyOnce<TDependency>(Expression<Action<TDependency>> expression) where TDependency : class
        {
            this.Mock<TDependency>().Verify(expression, Times.Once());
        }

        protected void VerifyNever<TDependency>(Expression<Action<TDependency>> expression) where TDependency : class
        {
            this.Mock<TDependency>().Verify(expression, Times.Never());
        }        

        [TearDown]
        public void Shutdown()
        {
            this.moqer.Dispose();
        }
    }
}
