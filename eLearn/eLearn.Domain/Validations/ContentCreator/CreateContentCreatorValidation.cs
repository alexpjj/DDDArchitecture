using eLearn.Domain.Validations.Commands.ContentCreator;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Validations.ContentCreator
{
    public class CreateContentCreatorValidation : AbstractValidator<CreateContentCreatorCommand>
    {
        public CreateContentCreatorValidation()
        {
            //Entity validation
            this.RuleFor(x => x.Name).NotNull();
            this.RuleFor(x => x.Surname).NotNull();
            this.RuleFor(x => x.Lastname).NotNull();

            //Context validation
            //TODO, check email is unique
        }        
    }
}
