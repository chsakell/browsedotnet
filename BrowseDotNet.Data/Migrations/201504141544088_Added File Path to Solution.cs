namespace BrowseDotNet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFilePathtoSolution : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solution", "FilePath", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solution", "FilePath");
        }
    }
}
