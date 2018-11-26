using eLearn.Domain.Contracts.Repositories;
using eLearn.Domain.Entities;
using eLearn.Infrastucture;
using eLearn.Infrastucture.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Infrastucture.Repositories
{
    public class UserRepository : Core.Repository<User, long>, IUserRepository
    {
        public UserRepository(IQueryableUnitOfWork IUnitOfWork) : base(IUnitOfWork)
        {

        }
    }
}
