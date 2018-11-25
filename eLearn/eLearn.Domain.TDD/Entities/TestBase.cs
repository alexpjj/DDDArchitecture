using AutoFixture;
using AutoFixture.Kernel;
using eLearn.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.TDD.Entities
{
    public class TestBase
    {
        protected Fixture fixture;

        [SetUp]
        public void Init()
        {            
            this.AutoFixtureConfiguration();
        }

        public void AutoFixtureConfiguration()
        {
            this.fixture = new Fixture();            
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
    }        
}
