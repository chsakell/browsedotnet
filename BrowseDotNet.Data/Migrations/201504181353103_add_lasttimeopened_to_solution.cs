namespace BrowseDotNet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_lasttimeopened_to_solution : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solution", "LastTimeOpened", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solution", "LastTimeOpened");
        }
    }
}
