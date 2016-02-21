namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_event_01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "NewEventInfo_EventId", "dbo.Event");
            DropIndex("dbo.User", new[] { "NewEventInfo_EventId" });
            DropColumn("dbo.User", "NewEventInfo_EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "NewEventInfo_EventId", c => c.Int());
            CreateIndex("dbo.User", "NewEventInfo_EventId");
            AddForeignKey("dbo.User", "NewEventInfo_EventId", "dbo.Event", "EventId");
        }
    }
}
