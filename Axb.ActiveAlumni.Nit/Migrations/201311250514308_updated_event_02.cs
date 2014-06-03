namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_event_02 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Event", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Event", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
