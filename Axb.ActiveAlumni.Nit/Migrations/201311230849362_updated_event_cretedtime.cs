namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_event_cretedtime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "CreatedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "CreatedOn");
        }
    }
}
