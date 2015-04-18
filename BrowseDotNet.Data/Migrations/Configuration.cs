namespace BrowseDotNet.Data.Migrations
{
    using BrowseDotNet.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BrowseDotNet.Data.BrowseDotNetDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BrowseDotNet.Data.BrowseDotNetDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //to avoid creating duplicate seed data. E.g.

            context.SolutionTypes.AddOrUpdate(
              new SolutionType { ID = 1, Type = "ASP.NET Web Application (MVC)", DateCreated = DateTime.Now },
              new SolutionType { ID = 2, Type = "Desktop Application (Windows Forms)", DateCreated = DateTime.Now },
              new SolutionType { ID = 3, Type = "ASP.NET Web Application (SPA)", DateCreated = DateTime.Now },
              new SolutionType { ID = 4, Type = "Desktop Application (Windows Forms)", DateCreated = DateTime.Now },
              new SolutionType { ID = 5, Type = "Unknown Application Type", DateCreated = DateTime.Now }
            );

            context.ProgrammingLanguages.AddOrUpdate(
                new ProgrammingLanguage { ID = 1, Name = "C#", DateCreated = DateTime.Now },
                new ProgrammingLanguage { ID = 2, Name = "Visual Basic", DateCreated = DateTime.Now },
                new ProgrammingLanguage { ID = 3, Name = "Javascript", DateCreated = DateTime.Now },
                new ProgrammingLanguage { ID = 4, Name = "jQuery", DateCreated = DateTime.Now },
                new ProgrammingLanguage { ID = 5, Name = "CSS", DateCreated = DateTime.Now },
                new ProgrammingLanguage { ID = 6, Name = "AngularJS", DateCreated = DateTime.Now }
                );

        }
    }
}
