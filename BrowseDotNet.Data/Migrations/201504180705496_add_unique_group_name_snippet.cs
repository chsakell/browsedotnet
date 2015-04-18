namespace BrowseDotNet.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class add_unique_group_name_snippet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Snippet", "GroupName", c => c.String(nullable: false, maxLength: 200,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "GroupName",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { Name: IX_GroupName, IsUnique: True }")
                    },
                }));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Snippet", "GroupName",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "GroupName", "IndexAnnotation: { Name: IX_GroupName, IsUnique: True }" },
                });
        }
    }
}
