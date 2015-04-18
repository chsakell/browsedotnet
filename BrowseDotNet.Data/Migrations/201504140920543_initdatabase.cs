namespace BrowseDotNet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.SearchKeys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Term = c.String(nullable: false, maxLength: 20),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Snippets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 300),
                        DateCreated = c.DateTime(nullable: false),
                        Website = c.String(maxLength: 200),
                        ProgrammingLanguageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProgrammingLanguages", t => t.ProgrammingLanguageID, cascadeDelete: true)
                .Index(t => t.ProgrammingLanguageID);
            
            CreateTable(
                "dbo.ProgrammingLanguages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Solutions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Author = c.String(maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 300),
                        Website = c.String(maxLength: 200),
                        DateRegistered = c.DateTime(nullable: false),
                        SolutionTypeID = c.Int(nullable: false),
                        ProgrammingLanguage_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SolutionTypes", t => t.SolutionTypeID, cascadeDelete: true)
                .ForeignKey("dbo.ProgrammingLanguages", t => t.ProgrammingLanguage_ID)
                .Index(t => t.SolutionTypeID)
                .Index(t => t.ProgrammingLanguage_ID);
            
            CreateTable(
                "dbo.SolutionTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.SnippetSearchKeys",
                c => new
                    {
                        Snippet_ID = c.Int(nullable: false),
                        SearchKey_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Snippet_ID, t.SearchKey_ID })
                .ForeignKey("dbo.Snippets", t => t.Snippet_ID, cascadeDelete: true)
                .ForeignKey("dbo.SearchKeys", t => t.SearchKey_ID, cascadeDelete: true)
                .Index(t => t.Snippet_ID)
                .Index(t => t.SearchKey_ID);
            
            CreateTable(
                "dbo.SolutionSearchKeys",
                c => new
                    {
                        Solution_ID = c.Int(nullable: false),
                        SearchKey_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Solution_ID, t.SearchKey_ID })
                .ForeignKey("dbo.Solutions", t => t.Solution_ID, cascadeDelete: true)
                .ForeignKey("dbo.SearchKeys", t => t.SearchKey_ID, cascadeDelete: true)
                .Index(t => t.Solution_ID)
                .Index(t => t.SearchKey_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.IdentityUserLogins", "User_Id", "dbo.Users");
            DropForeignKey("dbo.IdentityUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Solutions", "ProgrammingLanguage_ID", "dbo.ProgrammingLanguages");
            DropForeignKey("dbo.Solutions", "SolutionTypeID", "dbo.SolutionTypes");
            DropForeignKey("dbo.SolutionSearchKeys", "SearchKey_ID", "dbo.SearchKeys");
            DropForeignKey("dbo.SolutionSearchKeys", "Solution_ID", "dbo.Solutions");
            DropForeignKey("dbo.Snippets", "ProgrammingLanguageID", "dbo.ProgrammingLanguages");
            DropForeignKey("dbo.SnippetSearchKeys", "SearchKey_ID", "dbo.SearchKeys");
            DropForeignKey("dbo.SnippetSearchKeys", "Snippet_ID", "dbo.Snippets");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropIndex("dbo.SolutionSearchKeys", new[] { "SearchKey_ID" });
            DropIndex("dbo.SolutionSearchKeys", new[] { "Solution_ID" });
            DropIndex("dbo.SnippetSearchKeys", new[] { "SearchKey_ID" });
            DropIndex("dbo.SnippetSearchKeys", new[] { "Snippet_ID" });
            DropIndex("dbo.IdentityUserLogins", new[] { "User_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "UserId" });
            DropIndex("dbo.Solutions", new[] { "ProgrammingLanguage_ID" });
            DropIndex("dbo.Solutions", new[] { "SolutionTypeID" });
            DropIndex("dbo.Snippets", new[] { "ProgrammingLanguageID" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "UserId" });
            DropTable("dbo.SolutionSearchKeys");
            DropTable("dbo.SnippetSearchKeys");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.SolutionTypes");
            DropTable("dbo.Solutions");
            DropTable("dbo.ProgrammingLanguages");
            DropTable("dbo.Snippets");
            DropTable("dbo.SearchKeys");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
        }
    }
}
