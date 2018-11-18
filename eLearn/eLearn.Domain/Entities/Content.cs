using eLearn.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Entities
{
    public class Content : ValueObject<Content>
    {
        protected override bool EqualsCore(Content other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
