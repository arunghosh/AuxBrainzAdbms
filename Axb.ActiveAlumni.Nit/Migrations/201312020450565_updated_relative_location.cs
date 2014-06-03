namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_relative_location : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Relative", "Location", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Relative", "Location");
        }
    }
}
