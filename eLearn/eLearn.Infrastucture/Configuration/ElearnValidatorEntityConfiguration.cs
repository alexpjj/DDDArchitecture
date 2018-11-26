using eLearn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Infrastucture.Configuration
{
    public class ElearnValidatorEntityConfiguration : EntityTypeConfiguration<ElearnValidator>
    {
        public ElearnValidatorEntityConfiguration()
        {
            this.HasKey(x => x.Id);

            this.HasMany(x => x.Requests).WithOptional(x => x.Validator).HasForeignKey(x => x.ValidatorId);
        }
    }
}
