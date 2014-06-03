namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_event : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        CreatedUserId = c.Int(nullable: false),
                        CreatedUserName = c.String(maxLength: 128),
                        EventName = c.String(nullable: false, maxLength: 256),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        Location = c.String(maxLength: 256),
                        Description = c.String(maxLength: 1024),
                        IsDeleted = c.Boolean(nullable: false),
                        UserGroups = c.Byte(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.EventComment",
                c => new
                    {
                        EventCommentId = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        SenderId = c.Int(nullable: false),
                        SenderName = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        Text = c.String(nullable: false, maxLength: 1024),
                    })
                .PrimaryKey(t => t.EventCommentId)
                .ForeignKey("dbo.Event", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.EventInvitee",
                c => new
                    {
                        EventInviteeId = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 128),
                        Event_EventId = c.Int(),
                    })
                .PrimaryKey(t => t.EventInviteeId)
                .ForeignKey("dbo.Event", t => t.Event_EventId)
                .Index(t => t.Event_EventId);
            
            AddColumn("dbo.User", "NewEventInfo_EventId", c => c.Int());
            AddForeignKey("dbo.User", "NewEventInfo_EventId", "dbo.Event", "EventId");
            CreateIndex("dbo.User", "NewEventInfo_EventId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.EventInvitee", new[] { "Event_EventId" });
            DropIndex("dbo.EventComment", new[] { "EventId" });
            DropIndex("dbo.User", new[] { "NewEventInfo_EventId" });
            DropForeignKey("dbo.EventInvitee", "Event_EventId", "dbo.Event");
            DropForeignKey("dbo.EventComment", "EventId", "dbo.Event");
            DropForeignKey("dbo.User", "NewEventInfo_EventId", "dbo.Event");
            DropColumn("dbo.User", "NewEventInfo_EventId");
            DropTable("dbo.EventInvitee");
            DropTable("dbo.EventComment");
            DropTable("dbo.Event");
        }
    }
}
