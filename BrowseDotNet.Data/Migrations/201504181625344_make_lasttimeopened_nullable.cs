namespace BrowseDotNet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class make_lasttimeopened_nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Solution", "LastTimeOpened", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Solution", "LastTimeOpened", c => c.DateTime(nullable: false));
        }
    }
}
