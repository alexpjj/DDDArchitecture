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
    public class StudentService : Contracts.IStudentService
    {
        private readonly IStudentRepository studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<long> Create(CreateStudentDto studentDto)
        {
            var student = new Student(studentDto.Name, studentDto.Surname, studentDto.Lastname, studentDto.Email);

            this.studentRepository.Create(student);
            await this.studentRepository.IUnitOfWork.CommitAsync().ConfigureAwait(false);

            return student.Id;
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
