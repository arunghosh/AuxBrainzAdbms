namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_event_location_mndt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Event", "Location", c => c.String(nullable: false, maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Event", "Location", c => c.String(maxLength: 512));
        }
    }
}
