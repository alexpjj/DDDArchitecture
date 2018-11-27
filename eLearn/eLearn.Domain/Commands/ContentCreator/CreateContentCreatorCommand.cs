using eLearn.Domain.Contracts.Repositories;
using eLearn.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Validations.Commands.ContentCreator
{
    public class CreateContentCreatorCommand 
    {
        public readonly IContentCreatorRepository contentCreatorRepository;

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public PersonalEmail PersonalEmail { get; set; }

        public CreateContentCreatorCommand(IContentCreatorRepository contentCreatorRepository)
        {
            this.contentCreatorRepository = contentCreatorRepository;
        }
    }
}
