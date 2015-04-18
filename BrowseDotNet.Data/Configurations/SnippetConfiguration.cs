using BrowseDotNet.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Data.Configurations
{
    public class SnippetConfiguration : EntityTypeConfiguration<Snippet>
    {
        public SnippetConfiguration()
        {
            ToTable("Snippet");
            Property(s => s.Name).HasMaxLength(50).IsRequired();
            Property(s => s.Code).IsRequired();
            Property(s => s.Description).HasMaxLength(300).IsRequired();
            Property(s => s.Website).HasMaxLength(200);
            Property(s => s.GroupName).HasMaxLength(200).IsRequired()
                .HasColumnAnnotation("GroupName", new IndexAnnotation(new IndexAttribute("IX_GroupName") { IsUnique = true }));
        }
    }
}
