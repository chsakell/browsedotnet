namespace BrowseDotNet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_code_tosnippet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Snippet", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Snippet", "Code");
        }
    }
}
