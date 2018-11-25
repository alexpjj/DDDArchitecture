using eLearn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Infrastucture.Configuration
{
    public class ContentCreatorRequestEntityConfiguration : EntityTypeConfiguration<ContentCreatorRequest>
    {
        public ContentCreatorRequestEntityConfiguration()
        {
            this.HasKey(x => x.ContentCreatorId);
            this.Property(x => x.Status).IsRequired();

            this.Property(x => x.ValidatorId).IsOptional();
        }
    }
}
