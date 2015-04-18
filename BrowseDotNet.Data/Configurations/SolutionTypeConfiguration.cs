using BrowseDotNet.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Data.Configurations
{
    public class SolutionTypeConfiguration : EntityTypeConfiguration<SolutionType>
    {
        public SolutionTypeConfiguration()
        {
            ToTable("SolutionType");
            Property(s => s.Type).HasMaxLength(50).IsRequired();
        }
    }
}
