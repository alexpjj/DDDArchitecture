using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Application.Contracts
{
    public interface IUserService : IApplicationService
    {
        Task<long> Create(DTO.CreateUserDto studentDto);
        Task Update(long id, DTO.UpdateUserDto studentDto);
        Task Delete(long id);
    }
}   