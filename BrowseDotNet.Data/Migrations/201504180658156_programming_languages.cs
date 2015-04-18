namespace BrowseDotNet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class programming_languages : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SnippetSearchKeys", newName: "SearchKeySnippets");
            DropPrimaryKey("dbo.SearchKeySnippets");
            CreateTable(
                "dbo.ProgrammingLanguage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Snippet", "ProgrammingLanguageID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SearchKeySnippets", new[] { "SearchKey_ID", "Snippet_ID" });
            CreateIndex("dbo.Snippet", "ProgrammingLanguageID");
            AddForeignKey("dbo.Snippet", "ProgrammingLanguageID", "dbo.ProgrammingLanguage", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Snippet", "ProgrammingLanguageID", "dbo.ProgrammingLanguage");
            DropIndex("dbo.Snippet", new[] { "ProgrammingLanguageID" });
            DropPrimaryKey("dbo.SearchKeySnippets");
            DropColumn("dbo.Snippet", "ProgrammingLanguageID");
            DropTable("dbo.ProgrammingLanguage");
            AddPrimaryKey("dbo.SearchKeySnippets", new[] { "Snippet_ID", "SearchKey_ID" });
            RenameTable(name: "dbo.SearchKeySnippets", newName: "SnippetSearchKeys");
        }
    }
}
