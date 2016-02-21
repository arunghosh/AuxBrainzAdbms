namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_event_googleMap : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "GoogleMap", c => c.String(maxLength: 1024));
            AlterColumn("dbo.Event", "Location", c => c.String(maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Event", "Location", c => c.String(maxLength: 256));
            DropColumn("dbo.Event", "GoogleMap");
        }
    }
}
