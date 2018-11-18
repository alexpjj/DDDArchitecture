using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLearn.Application.DTO;
using eLearn.Domain.Contracts.Repositories;

namespace eLearn.Application.Providers
{
    public class StudentService : Contracts.IStudentService
    {

        public StudentService()
        {

        }

        public Task<long> Create(CreateStudentDto studentDto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task Update(long id, UpdateStudentDto studentDto)
        {
            throw new NotImplementedException();
        }
    }
}
