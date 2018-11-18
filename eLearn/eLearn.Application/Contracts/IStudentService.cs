using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Application.Contracts
{
    public interface IStudentService : IApplicationService
    {
        Task<long> Create(DTO.CreateStudentDto studentDto);
        Task Update(long id, DTO.UpdateStudentDto studentDto);
        Task Delete(long id);
    }
}   