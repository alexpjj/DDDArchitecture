using eLearn.Domain.Contracts.Repositories;
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
        private readonly IContentCreatorRepository contentCreatorRepository;
        public CreateContentCreatorValidation(IContentCreatorRepository contentCreatorRepository)
        {
            this.contentCreatorRepository = contentCreatorRepository;

            //Entity validation
            this.RuleFor(x => x.Name).NotNull();
            this.RuleFor(x => x.Surname).NotNull();
            this.RuleFor(x => x.Lastname).NotNull();

            //Context validation         
            var taskList = new List<Task>();
            taskList.Add(this.RuleFor(x => x.PersonalEmail.ElectronicAdress).EmailMustBeUnique(this.contentCreatorRepository));

            Task.WhenAll(taskList);
        }        
    }

    public static class EmailUniquenessValidator
    {
        public async static Task<IRuleBuilderOptions<T, string>> EmailMustBeUnique<T>(this IRuleBuilder<T, string> ruleBuilder, IContentCreatorRepository repository)
        {
            return ruleBuilder.MustAsync(async (request, email, token) =>
            {
                return await repository.CountAsync(c => c.PersonalEmail.ElectronicAdress == email) == 0;
            });
        }
    }
}
