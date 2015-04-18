namespace BrowseDotNet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_changed_to_dotnetuser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "DotNetUsers");
            DropForeignKey("dbo.IdentityUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.Users");
            DropIndex("dbo.IdentityUserRoles", new[] { "UserId" });
            DropIndex("dbo.IdentityUserClaims", new[] { "UserId" });
            RenameColumn(table: "dbo.IdentityUserLogins", name: "User_Id", newName: "DotNetUser_Id");
            RenameIndex(table: "dbo.IdentityUserLogins", name: "IX_User_Id", newName: "IX_DotNetUser_Id");
            AddColumn("dbo.IdentityUserRoles", "DotNetUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.IdentityUserClaims", "DotNetUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.IdentityUserClaims", "UserId", c => c.String());
            CreateIndex("dbo.IdentityUserRoles", "DotNetUser_Id");
            CreateIndex("dbo.IdentityUserClaims", "DotNetUser_Id");
            AddForeignKey("dbo.IdentityUserClaims", "DotNetUser_Id", "dbo.DotNetUsers", "Id");
            AddForeignKey("dbo.IdentityUserRoles", "DotNetUser_Id", "dbo.DotNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "DotNetUser_Id", "dbo.DotNetUsers");
            DropForeignKey("dbo.IdentityUserClaims", "DotNetUser_Id", "dbo.DotNetUsers");
            DropIndex("dbo.IdentityUserClaims", new[] { "DotNetUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "DotNetUser_Id" });
            AlterColumn("dbo.IdentityUserClaims", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.IdentityUserClaims", "DotNetUser_Id");
            DropColumn("dbo.IdentityUserRoles", "DotNetUser_Id");
            RenameIndex(table: "dbo.IdentityUserLogins", name: "IX_DotNetUser_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.IdentityUserLogins", name: "DotNetUser_Id", newName: "User_Id");
            CreateIndex("dbo.IdentityUserClaims", "UserId");
            CreateIndex("dbo.IdentityUserRoles", "UserId");
            AddForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IdentityUserClaims", "UserId", "dbo.Users", "Id");
            RenameTable(name: "dbo.DotNetUsers", newName: "Users");
        }
    }
}
