namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_event_picsurl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "PhotoUrl", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "PhotoUrl");
        }
    }
}
