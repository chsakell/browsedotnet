using BrowseDotNet.Data.Configurations;
using BrowseDotNet.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Data
{
    public class BrowseDotNetDbContext : IdentityDbContext<DotNetUser>
    {
        public BrowseDotNetDbContext() : base("BrowseDotNetEntities", throwIfV1Schema: false) { }

        static BrowseDotNetDbContext()
        {
            //Database.SetInitializer<BrowseDotNetDbContext>(new BrowseDotNetDbInitializer());
        }

        public IDbSet<Solution> Solutions { get; set; }
        public IDbSet<SearchKey> SearchKeys { get; set; }
        public IDbSet<Snippet> Snippets { get; set; }
        public IDbSet<SolutionType> SolutionTypes { get; set; }
        public IDbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        public static BrowseDotNetDbContext Create()
        {
            return new BrowseDotNetDbContext();
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SolutionConfiguration());
            modelBuilder.Configurations.Add(new SnippetConfiguration());
            modelBuilder.Configurations.Add(new SearchKeyConfiguration());
            modelBuilder.Configurations.Add(new ProgrammingLanguageConfiguration());
            modelBuilder.Configurations.Add(new SolutionTypeConfiguration());

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

        }
    }
}
