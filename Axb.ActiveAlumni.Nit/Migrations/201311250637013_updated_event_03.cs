namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_event_03 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventInvitee", "Event_EventId", "dbo.Event");
            DropIndex("dbo.EventInvitee", new[] { "Event_EventId" });
            AddColumn("dbo.EventInvitee", "EventId", c => c.Int(nullable: false));
            AddForeignKey("dbo.EventInvitee", "EventId", "dbo.Event", "EventId", cascadeDelete: true);
            CreateIndex("dbo.EventInvitee", "EventId");
            DropColumn("dbo.EventInvitee", "Event_EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EventInvitee", "Event_EventId", c => c.Int());
            DropIndex("dbo.EventInvitee", new[] { "EventId" });
            DropForeignKey("dbo.EventInvitee", "EventId", "dbo.Event");
            DropColumn("dbo.EventInvitee", "EventId");
            CreateIndex("dbo.EventInvitee", "Event_EventId");
            AddForeignKey("dbo.EventInvitee", "Event_EventId", "dbo.Event", "EventId");
        }
    }
}
