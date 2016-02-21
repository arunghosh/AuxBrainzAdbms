namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_activity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activity",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 2048),
                        OwnerId = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        StartedOn = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityId);
            
            CreateTable(
                "dbo.ActivityTask",
                c => new
                    {
                        ActivityTaskId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 256),
                        Description = c.String(maxLength: 2048),
                        ActivityId = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        PercentCompleted = c.Int(nullable: false),
                        ReminderFrequency = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ActivityTaskId)
                .ForeignKey("dbo.Activity", t => t.ActivityId, cascadeDelete: true)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.ActivityComment",
                c => new
                    {
                        ActivityCommentId = c.Int(nullable: false, identity: true),
                        ActivityId = c.Int(nullable: false),
                        SenderId = c.Int(nullable: false),
                        SenderName = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        Text = c.String(nullable: false, maxLength: 1024),
                    })
                .PrimaryKey(t => t.ActivityCommentId)
                .ForeignKey("dbo.Activity", t => t.ActivityId, cascadeDelete: true)
                .Index(t => t.ActivityId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ActivityComment", new[] { "ActivityId" });
            DropIndex("dbo.ActivityTask", new[] { "ActivityId" });
            DropForeignKey("dbo.ActivityComment", "ActivityId", "dbo.Activity");
            DropForeignKey("dbo.ActivityTask", "ActivityId", "dbo.Activity");
            DropTable("dbo.ActivityComment");
            DropTable("dbo.ActivityTask");
            DropTable("dbo.Activity");
        }
    }
}
