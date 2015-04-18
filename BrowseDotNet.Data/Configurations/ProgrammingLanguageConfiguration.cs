using BrowseDotNet.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Data.Configurations
{
    public class ProgrammingLanguageConfiguration : EntityTypeConfiguration<ProgrammingLanguage>
    {
        public ProgrammingLanguageConfiguration()
        {
            ToTable("ProgrammingLanguage");
            Property(p => p.Name).HasMaxLength(20).IsRequired();
        }
    }
}
