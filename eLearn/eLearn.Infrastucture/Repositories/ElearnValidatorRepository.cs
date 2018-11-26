using eLearn.Domain.Entities;
using eLearn.Infrastucture.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Infrastucture.Repositories
{
    public class ElearnValidatorRepository : Core.Repository<ElearnValidator, long>
    {
        public ElearnValidatorRepository(IQueryableUnitOfWork IUnitOfWork) : base(IUnitOfWork)
        {

        }
    }
}
