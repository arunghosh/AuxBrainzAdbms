namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_event_cnts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventInvitee", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventInvitee", "Date");
        }
    }
}
