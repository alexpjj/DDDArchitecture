using eLearn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Infrastucture.Configuration
{
    public class ContentCreatorEntityConfiguration : EntityTypeConfiguration<ContentCreator>
    {
        public ContentCreatorEntityConfiguration()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            this.Property(x => x.Name).IsRequired();
            this.Property(x => x.Surname).IsRequired();
            this.Property(x => x.Lastname).IsRequired();
            this.Property(x => x.PersonalEmail.ElectronicAdress).IsRequired().HasColumnName("PersonalEmail");
            this.Property(x => x.ProfessionalEmail.ElectronicAdress).HasColumnName("ElearnEmail");

            this.HasOptional(x => x.Request).WithRequired(x => x.ContentCreator);
            this.Ignore(x => x.Courses);
        }
    }
}
