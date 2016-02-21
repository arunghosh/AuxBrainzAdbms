namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_event_tentative : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "IsTentative", c => c.Boolean(nullable: false));
            AddColumn("dbo.Event", "SpecificGroupName", c => c.String(maxLength: 512));
            AddColumn("dbo.Event", "Batch", c => c.String(maxLength: 10));
            AddColumn("dbo.Event", "ExternalLink", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "ExternalLink");
            DropColumn("dbo.Event", "Batch");
            DropColumn("dbo.Event", "SpecificGroupName");
            DropColumn("dbo.Event", "IsTentative");
        }
    }
}
