using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Core
{
    public interface IUnitOfWorkTransaction
    {
        void Commit();

        void RollBack();
    }
}
