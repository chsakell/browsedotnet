using BrowseDotNet.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Data.Configurations
{
    public class SolutionConfiguration : EntityTypeConfiguration<Solution>
    {
        public SolutionConfiguration()
        {
            ToTable("Solution");
            Property(s => s.Author).HasMaxLength(50);
            Property(s => s.Name).HasMaxLength(100).IsRequired();
            Property(s => s.FilePath).HasMaxLength(250).IsRequired();
            Property(s => s.Description).HasMaxLength(300).IsRequired();
            Property(s => s.Website).HasMaxLength(200);
            Property(s => s.SolutionTypeID).IsRequired();
        }
    }
}
