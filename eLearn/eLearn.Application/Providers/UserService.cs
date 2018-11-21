using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLearn.Application.DTO;
using eLearn.Domain.Contracts.Repositories;
using eLearn.Domain.Entities;

namespace eLearn.Application.Providers
{
    public class UserService : Contracts.IUserService
    {
        private readonly IUserRepository studentRepository;

        public UserService(IUserRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<long> Create(CreateUserDto studentDto)
        {
            var student = new User(studentDto.Name, studentDto.Surname, studentDto.Lastname, studentDto.Email);

            this.studentRepository.Create(student);
            await this.studentRepository.IUnitOfWork.CommitAsync().ConfigureAwait(false);

            return student.Id;
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task Update(long id, UpdateUserDto studentDto)
        {
            throw new NotImplementedException();
        }
    }
}
