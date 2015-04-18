using BrowseDotNet.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Data.Configurations
{
    public class SearchKeyConfiguration : EntityTypeConfiguration<SearchKey>
    {
        public SearchKeyConfiguration()
        {
            ToTable("SearchKey");
            Property(s => s.Term).HasMaxLength(20).IsRequired();
        }
    }
}
