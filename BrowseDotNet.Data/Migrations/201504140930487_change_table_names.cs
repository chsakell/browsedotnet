namespace BrowseDotNet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_table_names : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SearchKeys", newName: "SearchKey");
            RenameTable(name: "dbo.Snippets", newName: "Snippet");
            RenameTable(name: "dbo.ProgrammingLanguages", newName: "ProgrammingLanguage");
            RenameTable(name: "dbo.Solutions", newName: "Solution");
            RenameTable(name: "dbo.SolutionTypes", newName: "SolutionType");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.SolutionType", newName: "SolutionTypes");
            RenameTable(name: "dbo.Solution", newName: "Solutions");
            RenameTable(name: "dbo.ProgrammingLanguage", newName: "ProgrammingLanguages");
            RenameTable(name: "dbo.Snippet", newName: "Snippets");
            RenameTable(name: "dbo.SearchKey", newName: "SearchKeys");
        }
    }
}
