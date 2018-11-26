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
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<long> Create(CreateUserDto studentDto)
        {
            var user = new User(studentDto.Name, studentDto.Surname, studentDto.Lastname, studentDto.Email);

            this.userRepository.Create(user);
            await this.userRepository.IUnitOfWork.CommitAsync().ConfigureAwait(false);

            return user.Id;
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
